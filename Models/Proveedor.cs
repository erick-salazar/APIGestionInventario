using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace APIGestionInventario.Models
{
    public class Proveedor
    {
        [Key]
        public int ProveedorId { get; set; }
        public string ProveedorNombre { get; set; } = null!;
        public bool Estado { get; set; } = true;
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public string CreadoPor { get; set; } = null!;
        public DateTime? FechaActulizado { get; set; }
        public string? ActualizadoPor { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrdenReposicion> OrdenesReposiciones { get; set; } = [];
        [JsonIgnore]
        public virtual ICollection<Producto> Productos { get; set; } = [];
    }
}
