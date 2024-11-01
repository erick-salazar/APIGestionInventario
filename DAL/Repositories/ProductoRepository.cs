using APIGestionInventario.DTOs.Custom;
using APIGestionInventario.Interfaces;
using APIGestionInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGestionInventario.DAL.Repositories
{
    public class ProductoRepository : RepositoyGestionInventarioDB<Producto>, IProductoRepository
    {
        private readonly GestionInventarioContext _GestionInventarioContext;
        public ProductoRepository(
            GestionInventarioContext gestionInventarioContext
        ) : base(gestionInventarioContext)
        {
            _GestionInventarioContext = gestionInventarioContext;
        }

        public async Task<GetAllResult<Producto>> ObtenerProductos(GetAllParameter getURLParametros)
        {

            List<Producto> productos = await _GestionInventarioContext.Productos
                .OrderBy(b => b.ProductoId)                
                .ToListAsync();

            List<Producto> productoLimiteSalto = productos
                .Skip(
                    getURLParametros.Salto ?? 0
                )
                .Take(
                    getURLParametros.Limite ?? 10
                ).ToList();

            return new GetAllResult<Producto>
            {
                ListaDetalle = productoLimiteSalto,
                TotalRegistro = productos.Count
            };
        }

        public async Task<GetAllResult<Producto>> ObtenerProductosCantidadMinima(GetAllParameter getURLParametros)
        {

            List<Producto> productos = await _GestionInventarioContext.Productos
                .Where(
                    x => x.ProductoCantidad <= x.ProductoCantidadMinima
                )
                .OrderBy(b => b.ProductoId)
                .ToListAsync();

            List<Producto> productoLimiteSalto = productos
                .Skip(
                    getURLParametros.Salto ?? 0
                )
                .Take(
                    getURLParametros.Limite ?? 10
                ).ToList();

            return new GetAllResult<Producto>
            {
                ListaDetalle = productoLimiteSalto,
                TotalRegistro = productos.Count
            };
        }

        
    }
}
