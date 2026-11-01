using Microsoft.Data.SqlClient;

namespace VideojuegosApi.Data;

// Clase que maneja la conexion a SQL Server
public class Conexion
{
    private readonly string _cadenaConexion;

    public Conexion(IConfiguration configuration)
    {
        _cadenaConexion = configuration.GetConnectionString("VideojuegosDB")
            ?? throw new InvalidOperationException("No se encontro la cadena de conexion 'VideojuegosDB'");
    }

    // Regresa una conexion nueva a la base de datos
    public SqlConnection ObtenerConexion()
    {
        return new SqlConnection(_cadenaConexion);
    }

    // Crea la base de datos y la tabla si todavia no existen
    public void InicializarBaseDeDatos()
    {
        var builder = new SqlConnectionStringBuilder(_cadenaConexion);
        string nombreBD = builder.InitialCatalog;

        builder.InitialCatalog = "master";
        using (var conexionMaster = new SqlConnection(builder.ConnectionString))
        {
            conexionMaster.Open();
            string sqlBD = $"IF DB_ID('{nombreBD}') IS NULL CREATE DATABASE [{nombreBD}]";
            using var cmd = new SqlCommand(sqlBD, conexionMaster);
            cmd.ExecuteNonQuery();
        }

        using var conexion = ObtenerConexion();
        conexion.Open();
        string sqlTabla = @"
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
            END";
        using var cmdTabla = new SqlCommand(sqlTabla, conexion);
        cmdTabla.ExecuteNonQuery();
    }
}
