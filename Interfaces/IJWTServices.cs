using APIGestionInventario.Models;

namespace APIGestionInventario.Interfaces
{
    public interface IJWTServices
    {
        string CrearTokenJWT(Usuario usuario);

        string? ObtenerClaimJWT(HttpRequest httpRequest, string claimNombre);
    }
}
