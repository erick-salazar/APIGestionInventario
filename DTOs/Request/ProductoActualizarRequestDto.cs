using System.ComponentModel.DataAnnotations;

namespace APIGestionInventario.DTOs.Request
{
    public class ProductoActualizarRequestDto
    {
        [Required]
        public int ProductoId { get; set; }

        [Required]
        public string ProductoNombre { get; set; } = null!;

        public string? ProductoDescripcion { get; set; }

        public decimal? ProductoPrecio { get; set; }

        public int? ProductoCantidad { get; set; }

        public int? ProductoCantidadMinima { get; set; }
        public int? ProveedoId { get; set; }
        public bool? Estado { get; set; }
    }
}
