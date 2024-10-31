using System.ComponentModel.DataAnnotations;

namespace APIGestionInventario.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; } = null!;
        public string ProductoDescripcion { get; set; } = null!;
        public decimal ProductoPrecio { get; set; }
        public int ProductoCantidad { get; set; }
        public int ProductoCantidadMinima { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public string CreadoPor { get; set; } = null!;
        public DateTime? FechaActulizado { get; set; }
        public string? ActualizadoPor {get; set;}

        public virtual ICollection<OrdenesCompra> OrdenesCompras { get; set; } = [];
    }
}
