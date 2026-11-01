using System.Data;
using Microsoft.Data.SqlClient;
using VideojuegosApi.Data;
using VideojuegosApi.Models;

namespace VideojuegosApi.Repositories;

// Repositorio que hace las operaciones CRUD con SQL Server
public class ProductoRepository : IProductoRepository
{
    private readonly Conexion _conexion;

    public ProductoRepository(Conexion conexion)
    {
        _conexion = conexion;
    }

    // Consulta todos los videojuegos
    public List<Producto> ObtenerTodos()
    {
        var lista = new List<Producto>();

        using var conexion = _conexion.ObtenerConexion();
        conexion.Open();

        string sql = "SELECT Id, Titulo, Plataforma, Genero, Precio, FechaLanzamiento FROM Productos ORDER BY Id";
        using var cmd = new SqlCommand(sql, conexion);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(MapearProducto(reader));
        }

        return lista;
    }

    // Busca un videojuego por su Id
    public Producto? ObtenerPorId(int id)
    {
        using var conexion = _conexion.ObtenerConexion();
        conexion.Open();

        string sql = "SELECT Id, Titulo, Plataforma, Genero, Precio, FechaLanzamiento FROM Productos WHERE Id = @Id";
        using var cmd = new SqlCommand(sql, conexion);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return MapearProducto(reader);
        }

        return null;
    }

    // Busca por titulo, plataforma o genero
    public List<Producto> Buscar(string texto)
    {
        var lista = new List<Producto>();

        using var conexion = _conexion.ObtenerConexion();
        conexion.Open();

        string sql = @"SELECT Id, Titulo, Plataforma, Genero, Precio, FechaLanzamiento
                       FROM Productos
                       WHERE Titulo LIKE @Texto OR Plataforma LIKE @Texto OR Genero LIKE @Texto
                       ORDER BY Titulo";
        using var cmd = new SqlCommand(sql, conexion);
        cmd.Parameters.Add("@Texto", SqlDbType.NVarChar, 102).Value = $"%{texto}%";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(MapearProducto(reader));
        }

        return lista;
    }

    // Inserta un videojuego y regresa el Id nuevo
    public int Insertar(Producto producto)
    {
        using var conexion = _conexion.ObtenerConexion();
        conexion.Open();

        string sql = @"INSERT INTO Productos (Titulo, Plataforma, Genero, Precio, FechaLanzamiento)
                       OUTPUT INSERTED.Id
                       VALUES (@Titulo, @Plataforma, @Genero, @Precio, @FechaLanzamiento)";
        using var cmd = new SqlCommand(sql, conexion);
        AgregarParametros(cmd, producto);

        return (int)cmd.ExecuteScalar();
    }

    // Actualiza los datos de un videojuego
    public bool Actualizar(Producto producto)
    {
        using var conexion = _conexion.ObtenerConexion();
        conexion.Open();

        string sql = @"UPDATE Productos
                       SET Titulo = @Titulo,
                           Plataforma = @Plataforma,
                           Genero = @Genero,
                           Precio = @Precio,
                           FechaLanzamiento = @FechaLanzamiento
                       WHERE Id = @Id";
        using var cmd = new SqlCommand(sql, conexion);
        AgregarParametros(cmd, producto);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = producto.Id;

        return cmd.ExecuteNonQuery() > 0;
    }

    // Elimina un videojuego por su Id
    public bool Eliminar(int id)
    {
        using var conexion = _conexion.ObtenerConexion();
        conexion.Open();

        string sql = "DELETE FROM Productos WHERE Id = @Id";
        using var cmd = new SqlCommand(sql, conexion);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        return cmd.ExecuteNonQuery() > 0;
    }

    // Convierte una fila de la tabla en un Producto
    private static Producto MapearProducto(SqlDataReader reader)
    {
        return new Producto
        {
            Id = reader.GetInt32(0),
            Titulo = reader.GetString(1),
            Plataforma = reader.GetString(2),
            Genero = reader.GetString(3),
            Precio = reader.GetDecimal(4),
            FechaLanzamiento = reader.GetDateTime(5)
        };
    }

    // Agrega los parametros que usan Insertar y Actualizar
    private static void AgregarParametros(SqlCommand cmd, Producto producto)
    {
        cmd.Parameters.Add("@Titulo", SqlDbType.NVarChar, 100).Value = producto.Titulo;
        cmd.Parameters.Add("@Plataforma", SqlDbType.NVarChar, 50).Value = producto.Plataforma;
        cmd.Parameters.Add("@Genero", SqlDbType.NVarChar, 50).Value = producto.Genero ?? string.Empty;
        cmd.Parameters.Add("@Precio", SqlDbType.Decimal).Value = producto.Precio;
        cmd.Parameters.Add("@FechaLanzamiento", SqlDbType.Date).Value = producto.FechaLanzamiento.Date;
    }
}
