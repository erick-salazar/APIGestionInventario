using APIGestionInventario.Models;

namespace APIGestionInventario.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> Login(string usuarioId, string password);

        Task<Usuario?> RefreshToken(string usuarioId);
    }
}
