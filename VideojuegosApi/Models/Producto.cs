using System.ComponentModel.DataAnnotations;

namespace VideojuegosApi.Models;

// Entidad que representa un videojuego dentro de la tienda
public class Producto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El titulo es obligatorio")]
    [StringLength(100)]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La plataforma es obligatoria")]
    [StringLength(50)]
    public string Plataforma { get; set; } = string.Empty;

    [StringLength(50)]
    public string Genero { get; set; } = string.Empty;

    [Range(0, 99999.99, ErrorMessage = "El precio debe ser mayor o igual a 0")]
    public decimal Precio { get; set; }

    public DateTime FechaLanzamiento { get; set; }
}
