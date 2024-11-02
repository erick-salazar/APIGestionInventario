# APIGestionInventario

## Ejecutar en desarrollo

1. Clonar el repositorio
2. Levantar la base de datos con docker o cambiar string de conexion

    2.1 Docker-Compose para levantar la base de datos SQL
    ```
    docker-compose up -d
    ```
3. Actualizar la base de datos con las migraciones en visual studio en PackageManagerConsole:
```
Update-Database
```
4. Pudes cambiar lo siguiente en la configuracion de la aplicacion a lo requerido en el archivo ```appsettings.json``` de cada seccion
* Conexion de la base de datos  ```GestionInventarioDB```
* JWT configuracion ```Jwt```
* Configuracion de solicitudes en cache ```MemoryCacheEntryOptions```

5. Ejecutar la aplicacion en dev para visual estudio: 
```
F5 
```

### Stack usado
* SQL
* .NET8