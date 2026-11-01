using VideojuegosApi.Models;

namespace VideojuegosApi.Repositories;

public interface IProductoRepository
{
    List<Producto> ObtenerTodos();
    Producto? ObtenerPorId(int id);
    int Insertar(Producto producto);
    bool Actualizar(Producto producto);
    bool Eliminar(int id);

    // Metodo extra para buscar por titulo, plataforma o genero
    List<Producto> Buscar(string texto);
}
