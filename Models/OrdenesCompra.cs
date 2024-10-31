using System.ComponentModel.DataAnnotations;

namespace APIGestionInventario.Models
{
    public class OrdenesCompra
    {
        [Key]
        public int OrdenCompraId { get; set; }
        public int ProductoId { get; set; }
        public int ProductoCantidad { get; set; }
        public decimal ProductoPrecio { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public string CreadoPor { get; set; } = null!;
        public DateTime? FechaActulizado { get; set; }
        public string? ActualizadoPor { get; set; }
        public virtual Producto Productos { get; set; } = null!;
    }
}
