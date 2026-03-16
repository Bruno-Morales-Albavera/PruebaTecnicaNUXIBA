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
totals AS (
    SELECT
        User_id,
        SUM(DATEDIFF(SECOND, login_time, logout_time)) AS total_seconds
    FROM sessions
    GROUP BY User_id
)
SELECT TOP 1
    User_id,
    CONCAT(
        total_seconds / 86400, ' días, ',
        (total_seconds % 86400) / 3600, ' horas, ',
        (total_seconds % 3600) / 60, ' minutos, ',
        total_seconds % 60, ' segundos'
    ) AS tiempo_total
FROM totals
ORDER BY total_seconds ASC;