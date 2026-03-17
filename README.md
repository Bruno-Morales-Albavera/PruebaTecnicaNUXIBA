Bruno Morales Albavera:
Pasos para ejecutar la base de datos.

Se estuvo trabajando con el contenedor docker que se proporcionó durante la prueba: docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourStrong!Passw0rd'    -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2019-latest

Dentro del repositorio se encuentra una carpeta con nombre "Ejercicio2_SQL" en dónde primordialmente se encuentran dos scripts con el nombre "CCenterRIA" y "CCenterRIA2". Es importante ejecutar ambos scripts.
Para los ejercicios 1 y 3 se utiliza la base de datos "CCenterRIA2"; Por otro lado, se utiliza "CCenterRIA" para el ejercicio 2, la única diferencia entre estas, es la cantidad de datos para una mejor consulta.

A continuación se lista la fumcionalidad de los endpoints elaborados:

GET    | http://localhost:5139/Logins          | Obtiene todos los registros de login y logout de la tabla ccloglogin.

POST   | http://localhost:5139/Logins          | Registra un nuevo movimiento de login o logout. Valida que TipoMov sea 0 (logout) o 1 (login) y que la fecha no esté vacía ni sea futura.

PUT    | http://localhost:5139/Logins/{id}     | Actualiza un registro existente por su id. El id de la URL debe coincidir con el id del body. Valida TipoMov y fecha igual que el POST.

DELETE | http://localhost:5139/Logins/{id}     | Elimina un registro de login o logout por su id. Si el registro no existe devuelve 404 Not Found.

GET    | http://localhost:5139/Logins/export   | Genera y descarga un archivo reporte_horas.csv con el nombre de usuario, nombre completo, área y total de horas trabajadas por cada usuario, cruzando datos de ccloglogin,                                                     ccUsers y ccRIACat_Areas.
