using APIGestionInventario.DTOs.Custom;
using APIGestionInventario.DTOs.Request;
using APIGestionInventario.Interfaces;
using APIGestionInventario.Middleware;
using APIGestionInventario.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;

namespace APIGestionInventario.DAL.Repositories
{
    public class OrdenCompraRepository : RepositoyGestionInventarioDB<OrdenCompra>, IOrdenCompraRepository
    {
        private readonly GestionInventarioContext _GestionInventarioContext;
        private readonly IProductoRepository _IProductoRepository;
        private readonly IOrdenReposicionRepository _IOrdenReposicionRepository;

        public OrdenCompraRepository(
            GestionInventarioContext gestionInventarioContext,
            IProductoRepository productoRepository,
            IOrdenReposicionRepository ordenReposicionRepository
        ) : base(gestionInventarioContext)
        {
            _GestionInventarioContext = gestionInventarioContext;
            _IProductoRepository = productoRepository;
            _IOrdenReposicionRepository = ordenReposicionRepository;
        }

        public async Task<GetAllResult<OrdenCompra>> ObtenerOrdenesCompra(GetAllParameter getURLParametros)
        {
            List<OrdenCompra> ordenCompra = await _GestionInventarioContext.OrdenesCompras
                .AsNoTracking()
                .OrderBy(b => b.OrdenCompraId)
                .ToListAsync();

            List<OrdenCompra> ordenCompraLimiteSalto = ordenCompra
                .Skip(
                    getURLParametros.Salto ?? 0
                )
                .Take(
                    getURLParametros.Limite ?? 10
                ).ToList();

            return new GetAllResult<OrdenCompra>
            {
                ListaDetalle = ordenCompraLimiteSalto,
                TotalRegistro = ordenCompra.Count
            };
        }

        public async Task<OrdenCompra> CrearOrdenCompra(OrdenCompraCrearRequestDto ordenCompraCrearRequestDto, string usuarioId)
        {
            Producto? producto = await _IProductoRepository.GetByIdAsync(ordenCompraCrearRequestDto.ProductoId) ?? throw new CustomError((int)HttpStatusCode.NotFound, "0006", ($"Producto con ID {ordenCompraCrearRequestDto.ProductoId} no encontrado."), null);

            if (producto.ProductoCantidad < ordenCompraCrearRequestDto.ProductoCantidad)
            {
                throw new CustomError((int)HttpStatusCode.BadRequest, "0008", ($"Producto: {producto.ProductoNombre} con inventario menor a la orden de compra: {ordenCompraCrearRequestDto.ProductoCantidad}."), null);
            }

            OrdenCompra ordenCompra = new()
            {
                ProductoId = ordenCompraCrearRequestDto.ProductoId,
                ProductoCantidad = ordenCompraCrearRequestDto.ProductoCantidad,
                ProductoPrecio = producto.ProductoPrecio,
                CreadoPor = usuarioId
            };

            await _GestionInventarioContext.Database.BeginTransactionAsync();
            try
            {
                producto.ProductoCantidad -= ordenCompraCrearRequestDto.ProductoCantidad;

                _IProductoRepository.Update(producto);
                await _GestionInventarioContext.AddAsync(ordenCompra);

                await _GestionInventarioContext.SaveChangesAsync();
                await _IOrdenReposicionRepository.CrearOrdenReposionAutomatica(producto, ordenCompra.OrdenCompraId, usuarioId);

                await _GestionInventarioContext.SaveChangesAsync();

                await _GestionInventarioContext.Database.CommitTransactionAsync();

                return ordenCompra;
            }
            catch (DbException ex)
            {
                await _GestionInventarioContext.Database.RollbackTransactionAsync();
                ex.ToString();
                throw;
            }
        }

        public async Task<OrdenCompra?> GetByOrdenProductoIdAsync(int ordenCompraId, int productoId)
        {
            return await _GestionInventarioContext.OrdenesCompras
                .AsNoTracking()
                .Where(
                    x => x.OrdenCompraId == ordenCompraId
                    && x.ProductoId == productoId
                )
                .FirstOrDefaultAsync();
        }

        public async Task<OrdenCompra> ActualizarOrdenCompra(OrdenCompraActualizarRequestDto ordenCompraActualizarRequestDto, string usuarioId)
        {
            await _GestionInventarioContext.Database.BeginTransactionAsync();

            OrdenCompra? ordenCompra = await this.GetByIdAsync(ordenCompraActualizarRequestDto.OrdenCompraId);
            Producto? producto = await _IProductoRepository.GetByIdAsync(ordenCompraActualizarRequestDto.ProductoId) ?? throw new CustomError((int)HttpStatusCode.NotFound, "0006", ($"Producto con ID {ordenCompraActualizarRequestDto.ProductoId} no encontrado."), null);

            try
            {
                producto.ProductoCantidad = ordenCompra!.ProductoCantidad - ordenCompraActualizarRequestDto.ProductoCantidad;

                ordenCompra.ProductoCantidad = ordenCompraActualizarRequestDto.ProductoCantidad;
                ordenCompra.ActualizadoPor = usuarioId;
                ordenCompra.FechaActulizado = DateTime.Now;

                _IProductoRepository.Update(producto);

                this.Update(ordenCompra);
                await _IOrdenReposicionRepository.CrearOrdenReposionAutomatica(producto, ordenCompra.OrdenCompraId, usuarioId);
                await _GestionInventarioContext.SaveChangesAsync();

                await _GestionInventarioContext.Database.CommitTransactionAsync();

                return ordenCompra;
            }
            catch (DbException ex)
            {
                await _GestionInventarioContext.Database.RollbackTransactionAsync();
                ex.ToString();
                throw;
            }
        }
    }
}
