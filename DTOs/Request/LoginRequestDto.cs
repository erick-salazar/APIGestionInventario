
using System.ComponentModel.DataAnnotations;

namespace APIGestionInventario.DTOs.Request
{
    public class LoginRequestDto
    {
        [Required]
        [MinLength(4)]
        public string UsuarioId { get; set; } = null!;

        [Required]
        [MinLength(4)]
        public string Password { get; set; } = null!;
    }
}
