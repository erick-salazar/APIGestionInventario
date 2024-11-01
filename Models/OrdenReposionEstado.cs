using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace APIGestionInventario.Models
{
    public class OrdenReposionEstado
    {
        [Key]
        public int OrdenReposicionEstadoId { get; set; }
        public string EstadoNombre { get; set; } = null!;
        public bool Estado { get; set; } = true;
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public string CreadoPor { get; set; } = null!;
        public DateTime? FechaActulizado { get; set; }
        public string? ActualizadoPor { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrdenReposicion> OrdenesReposiciones { get; set; } = [];
    }
}
