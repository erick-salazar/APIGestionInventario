using System.ComponentModel.DataAnnotations;

namespace APIGestionInventario.Models
{
    public class Usuario
    {
        [Key]
        public string UsuarioId { get; set; } = null!;
        public string Password { get; set; }=null!;
        public string UsuarioNombre { get; set; } = null!;
        public string UsuarioApellido{ get; set; } = null!;
        public int RolId { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public string CreadoPor { get; set; } = null!;
        public DateTime? FechaActulizado { get; set; }
        public string? ActualizadoPor { get; set; }       
        public virtual Rol Roles { get; set; } = null!;

    }
}
