using APIGestionInventario.Models;

namespace APIGestionInventario.Interfaces
{
    public interface IUsuarioRepository : IRepositoyGestionInventarioDB<Usuario>
    {
        Task<Usuario?> Login(string usuarioId, string password);

        Task<Usuario?> RefreshToken(string usuarioId);
    }
}
