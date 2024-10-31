namespace APIGestionInventario.DTOs.Response
{
    public class LoginResponseDto
    {
        public string UsuarioId { get; set; } = null!;
        public string UsuarioNombre { get; set; } = null!;
        public string UsuarioApellido { get; set; } = null!;
        public int RolId { get; set; }
        public string Token { get; set; } = null!;
    }
}
