using APIGestionInventario.DTOs.Custom;
using APIGestionInventario.Interfaces;
using APIGestionInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGestionInventario.DAL.Repositories
{
    public class OrdenReposicionRepository : RepositoyGestionInventarioDB<OrdenReposicion>, IOrdenReposicionRepository
    {
        private readonly GestionInventarioContext _GestionInventarioContext;
        private readonly IProductoRepository _IProductoRepository;

        public OrdenReposicionRepository(
            GestionInventarioContext gestionInventarioContext,
            IProductoRepository productoRepository
        ) : base(gestionInventarioContext)
        {
            _GestionInventarioContext = gestionInventarioContext;
            _IProductoRepository = productoRepository;
        }

        public async Task<GetAllResult<OrdenReposicion>> ObtenerOrdenReposicion(GetAllParameter getURLParametros)
        {

            List<OrdenReposicion> ordenReposicion = await _GestionInventarioContext.OrdenesReposiciones
                .Where(
                    x => x.OrdenReposiconEstadoId==1
                )
                .OrderBy(b => b.ProductoId)
                .ToListAsync();

            List<OrdenReposicion> productoLimiteSalto = ordenReposicion
                .Skip(
                    getURLParametros.Salto ?? 0
                )
                .Take(
                    getURLParametros.Limite ?? 10
                ).ToList();

            return new GetAllResult<OrdenReposicion>
            {
                ListaDetalle = productoLimiteSalto,
                TotalRegistro = ordenReposicion.Count
            };
        }

        public async Task<OrdenReposicion?> CrearOrdenReposionAutomatica(Producto producto, int ordenCompraId, string usuarioId)
        {
            OrdenReposicion? ordenReposicion = await this.GetByIdAsync(ordenCompraId);

            if (producto.ProductoCantidad <= producto.ProductoCantidadMinima)
            {
                int productoCantidad = (producto.ProductoCantidadMinima * 2);

                if (ordenReposicion == null)
                {
                    ordenReposicion = new()
                    {
                        ProductoId = producto.ProductoId,
                        ProveedorId = producto.ProductoId,
                        ProductoCantidad = productoCantidad,
                        CreadoPor = usuarioId
                    };
                    await this.AddAsync(ordenReposicion);
                }
                else {

                    ordenReposicion.ProductoCantidad = productoCantidad;
                    ordenReposicion.ActualizadoPor = usuarioId;
                    ordenReposicion.FechaActulizado = DateTime.Now;

                    this.Update(ordenReposicion);
                }
            }
            else
            {
                if (ordenReposicion != null)
                {
                    ordenReposicion.OrdenReposiconEstadoId = 3;
                    ordenReposicion.ActualizadoPor = usuarioId;
                    ordenReposicion.FechaActulizado = DateTime.Now;

                    this.Update(ordenReposicion);
                }
            }

            return ordenReposicion;
        }

        public async Task<OrdenReposicion> ActualizarOrdenReposionAutomatica(Producto producto, string usuarioId)
        {
            OrdenReposicion ordenReposicion = new();
            if (producto.ProductoCantidad <= producto.ProductoCantidadMinima)
            {
                ordenReposicion = new()
                {
                    ProductoId = producto.ProductoId,
                    ProveedorId = producto.ProductoId,
                    ProductoCantidad = (producto.ProductoCantidadMinima * 2),
                    CreadoPor = usuarioId
                };

                await this.AddAsync(ordenReposicion);
            }

            return ordenReposicion;
        }
    }
}
