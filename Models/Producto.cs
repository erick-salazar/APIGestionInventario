using System.Text.Json.Serialization;
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
        public bool Estado { get; set; } = true;
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public string CreadoPor { get; set; } = null!;
        public DateTime? FechaActulizado { get; set; }
        public string? ActualizadoPor {get; set;}
        public int ProveedorId { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrdenCompra> OrdenesCompras { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<OrdenReposicion> OrdenesReposiciones { get; set; } = null!;

        [JsonIgnore]
        public virtual Proveedor Proveedores { get; set; } = null!;
    }
}
