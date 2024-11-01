using APIGestionInventario.DTOs.Custom;
using APIGestionInventario.Models;

namespace APIGestionInventario.Interfaces
{
    public interface IOrdenReposicionRepository : IRepositoyGestionInventarioDB<OrdenReposicion>
    {
        Task<GetAllResult<OrdenReposicion>> ObtenerOrdenReposicion(GetAllParameter getURLParametros);
        Task<OrdenReposicion?> CrearOrdenReposionAutomatica(Producto producto, int ordenCompraId, string usuarioId);

    }
}
