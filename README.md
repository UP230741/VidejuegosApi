# API de Videojuegos - CRUD con SQL Server

Backend en ASP.NET Core Web API que administra productos de una tienda de videojuegos usando SQL Server y ADO.NET. Se prueba con Swagger.

## Tecnologias

- .NET 10 - ASP.NET Core Web API
- SQL Server LocalDB
- ADO.NET con Microsoft.Data.SqlClient
- Swagger con Swashbuckle

## Estructura

```
VideojuegosApi/
├── Controllers/ProductosController.cs   Endpoints de la API
├── Data/Conexion.cs                     Conexion a SQL Server
├── Models/Producto.cs                   Modelo del videojuego
├── Repositories/IProductoRepository.cs  Interface del repositorio
├── Repositories/ProductoRepository.cs   Consultas, insert, update y delete
├── Database/script.sql                  Script de la base de datos
└── Program.cs                           Configuracion e inicio de la API
```

## Como ejecutarlo

1. Tener instalado el SDK de .NET 10 y SQL Server LocalDB.
2. Entrar a la carpeta del proyecto y ejecutar:

```bash
cd VideojuegosApi
dotnet run
```

3. Se abre Swagger en `http://localhost:5181/swagger`.

La base de datos `VideojuegosDB` y la tabla `Productos` se crean solas la primera vez que se ejecuta la API. Tambien se puede crear manualmente con `Database/script.sql`.

## Endpoints

| Metodo | Ruta | Descripcion |
|---|---|---|
| GET | /api/productos | Lista todos los videojuegos |
| GET | /api/productos/{id} | Obtiene un videojuego por Id |
| GET | /api/productos/buscar?texto= | Busca por titulo, plataforma o genero |
| POST | /api/productos | Registra un videojuego |
| PUT | /api/productos/{id} | Actualiza un videojuego |
| DELETE | /api/productos/{id} | Elimina un videojuego |

## Flujo del proyecto

<!-- Escribe aqui con tus palabras el flujo y los metodos del proyecto -->
