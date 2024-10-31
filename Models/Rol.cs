using System.ComponentModel.DataAnnotations;

namespace APIGestionInventario.Models
{
    public class Rol
    {
        [Key]
        public int RolId { get; set; }
        public string RolNombre { get; set; } = null!;        
        public bool Estado { get; set; }
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public string CreadoPor { get; set; } = null!;
        public DateTime? FechaActulizado { get; set; }
        public string? ActualizadoPor { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; } = [];
    }
}
