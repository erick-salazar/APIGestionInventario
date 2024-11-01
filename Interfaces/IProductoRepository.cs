using APIGestionInventario.DTOs.Custom;
using APIGestionInventario.Models;

namespace APIGestionInventario.Interfaces
{
    public interface IProductoRepository : IRepositoyGestionInventarioDB<Producto>
    {
        Task<GetAllResult<Producto>> ObtenerProductos(GetAllParameter getURLParametros);

        Task<GetAllResult<Producto>> ObtenerProductosCantidadMinima(GetAllParameter getURLParametros);
    }
}
