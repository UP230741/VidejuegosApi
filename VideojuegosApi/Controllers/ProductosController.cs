using Microsoft.AspNetCore.Mvc;
using VideojuegosApi.Models;
using VideojuegosApi.Repositories;

namespace VideojuegosApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoRepository _repositorio;

    public ProductosController(IProductoRepository repositorio)
    {
        _repositorio = repositorio;
    }

    // Lista todos los videojuegos
    [HttpGet]
    public ActionResult<List<Producto>> ObtenerTodos()
    {
        return Ok(_repositorio.ObtenerTodos());
    }

    // Obtiene un videojuego por Id
    [HttpGet("{id:int}")]
    public ActionResult<Producto> ObtenerPorId(int id)
    {
        var producto = _repositorio.ObtenerPorId(id);
        if (producto == null)
        {
            return NotFound(new { mensaje = $"No existe el producto con Id {id}" });
        }
        return Ok(producto);
    }

    // Busca videojuegos por texto
    [HttpGet("buscar")]
    public ActionResult<List<Producto>> Buscar([FromQuery] string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            return BadRequest(new { mensaje = "Debes enviar un texto para buscar" });
        }
        return Ok(_repositorio.Buscar(texto.Trim()));
    }

    // Registra un videojuego nuevo
    [HttpPost]
    public ActionResult<Producto> Insertar([FromBody] Producto producto)
    {
        int nuevoId = _repositorio.Insertar(producto);
        producto.Id = nuevoId;
        return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoId }, producto);
    }

    // Actualiza un videojuego
    [HttpPut("{id:int}")]
    public IActionResult Actualizar(int id, [FromBody] Producto producto)
    {
        producto.Id = id;
        bool actualizado = _repositorio.Actualizar(producto);
        if (!actualizado)
        {
            return NotFound(new { mensaje = $"No existe el producto con Id {id}" });
        }
        return Ok(new { mensaje = "Producto actualizado correctamente" });
    }

    // Elimina un videojuego
    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
    {
        bool eliminado = _repositorio.Eliminar(id);
        if (!eliminado)
        {
            return NotFound(new { mensaje = $"No existe el producto con Id {id}" });
        }
        return Ok(new { mensaje = "Producto eliminado correctamente" });
    }
}
