Bruno Morales Albavera:
Pasos para ejecutar la base de datos.

Se estuvo trabajando con el contenedor docker que se proporcionó durante la prueba: docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourStrong!Passw0rd'    -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2019-latest

Dentro del repositorio se encuentra una carpeta con nombre "Ejercicio2_SQL" en dónde primordialmente se encuentran dos scripts con el nombre "CCenterRIA" y "CCenterRIA2". Es importante ejecutar ambos scripts.
Para los ejercicios 1 y 3 se utiliza la base de datos "CCenterRIA2"; Por otro lado, se utiliza "CCenterRIA" para el ejercicio 2, la única diferencia entre estas, es la cantidad de datos para una mejor consulta.
