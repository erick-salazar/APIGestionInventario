using APIGestionInventario.DTOs.Custom;
using APIGestionInventario.DTOs.Request;
using APIGestionInventario.Models;
using System.Threading.Tasks;

namespace APIGestionInventario.Interfaces
{
    public interface IOrdenCompraRepository : IRepositoyGestionInventarioDB<OrdenCompra>
    {
        Task<GetAllResult<OrdenCompra>> ObtenerOrdenesCompra(GetAllParameter getURLParametros);

        Task<OrdenCompra> CrearOrdenCompra(OrdenCompraCrearRequestDto ordenCompraCrearRequestDto, string usuarioId);

        Task<OrdenCompra?> GetByOrdenProductoIdAsync(int ordenCompraId, int productoId);

        Task<OrdenCompra> ActualizarOrdenCompra(OrdenCompraActualizarRequestDto ordenCompraActualizarRequestDto, string usuarioId);
    }
}
