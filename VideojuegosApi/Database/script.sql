-- Script para crear la base de datos y la tabla
IF DB_ID('VideojuegosDB') IS NULL
    CREATE DATABASE VideojuegosDB;
GO

USE VideojuegosDB;
GO

IF OBJECT_ID('dbo.Productos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Productos (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Titulo NVARCHAR(100) NOT NULL,
        Plataforma NVARCHAR(50) NOT NULL,
        Genero NVARCHAR(50) NOT NULL DEFAULT '',
        Precio DECIMAL(10,2) NOT NULL,
        FechaLanzamiento DATE NOT NULL
    );

    INSERT INTO dbo.Productos (Titulo, Plataforma, Genero, Precio, FechaLanzamiento) VALUES
    ('The Legend of Zelda: Tears of the Kingdom', 'Nintendo Switch', 'Aventura', 1399.00, '2023-05-12'),
    ('God of War Ragnarok', 'PlayStation 5', 'Accion', 1199.00, '2022-11-09'),
    ('Forza Horizon 5', 'Xbox Series X', 'Carreras', 999.00, '2021-11-09'),
    ('Minecraft', 'PC', 'Sandbox', 549.00, '2011-11-18');
END
GO
