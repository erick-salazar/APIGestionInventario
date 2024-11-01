using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace APIGestionInventario.Models
{
    public class OrdenCompra
    {
        [Key]
        public int OrdenCompraId { get; set; }
        public int ProductoId { get; set; }
        public int ProductoCantidad { get; set; }
        public decimal ProductoPrecio { get; set; }
        public bool Estado { get; set; } = true;
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public string CreadoPor { get; set; } = null!;
        public DateTime? FechaActulizado { get; set; }
        public string? ActualizadoPor { get; set; }
        public virtual Producto Productos { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<OrdenReposicion> OrdenesReposiciones { get; set; } = [];
    }
}
