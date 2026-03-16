WITH ranked AS (
    SELECT
        User_id,
        Fechaa,
        TipoMov,
        SUM(CASE WHEN TipoMov = 1 THEN 1 ELSE 0 END)
            OVER (PARTITION BY User_id ORDER BY Fechaa) AS session_num
    FROM ccloglogin
),
sessions AS (
    SELECT
        User_id,
        session_num,
        MAX(CASE WHEN TipoMov = 1 THEN Fechaa END) AS login_time,
        MAX(CASE WHEN TipoMov = 0 THEN Fechaa END) AS logout_time
    FROM ranked
    GROUP BY User_id, session_num
    HAVING MAX(CASE WHEN TipoMov = 1 THEN Fechaa END) IS NOT NULL
       AND MAX(CASE WHEN TipoMov = 0 THEN Fechaa END) IS NOT NULL
),
session_seconds AS (
    SELECT
        User_id,
        YEAR(login_time)  AS anio,
        MONTH(login_time) AS mes,
        DATEDIFF(SECOND, login_time, logout_time) AS seconds
    FROM sessions
),
averages AS (
    SELECT
        User_id,
        anio,
        mes,
        AVG(seconds) AS avg_seconds
    FROM session_seconds
    GROUP BY User_id, anio, mes
)
SELECT TOP 1
    User_id,
    anio,
    mes,
    CONCAT(
        avg_seconds / 86400, ' días, ',
        (avg_seconds % 86400) / 3600, ' horas, ',
        (avg_seconds % 3600) / 60, ' minutos, ',
        avg_seconds % 60, ' segundos'
    ) AS promedio_sesion
FROM averages
ORDER BY User_id, anio, mes;