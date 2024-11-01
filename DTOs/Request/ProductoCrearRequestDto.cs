using System.ComponentModel.DataAnnotations;

namespace APIGestionInventario.DTOs.Request
{
    public class ProductoCrearRequestDto
    {

        [Required]
        [MinLength(1)]
        public string ProductoNombre { get; set; } = null!;

        [Required]
        [MinLength(1)]
        public string ProductoDescripcion { get; set; } = null!;

        [Required]
        public decimal ProductoPrecio { get; set; }

        [Required]
        public int ProductoCantidad { get; set; }

        [Required]
        public int ProductoCantidadMinima { get; set; }

        [Required]
        public int ProveedoId { get; set; }
        public bool? Estado { get; set; } = true;
    }
}
