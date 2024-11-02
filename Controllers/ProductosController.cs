using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIGestionInventario.Models;
using Microsoft.AspNetCore.Authorization;
using APIGestionInventario.Interfaces;
using APIGestionInventario.DTOs.Custom;
using APIGestionInventario.DTOs.Response;
using System.Diagnostics;
using APIGestionInventario.DTOs.Request;
using APIGestionInventario.Middleware;
using System.Net;
using Microsoft.Extensions.Caching.Memory;

namespace APIGestionInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {

        private readonly IProductoRepository _IProductoRepository;
        private readonly IJWTServices _IJWTServices;
        private readonly IGeneralServices _IGeneralServices;
        private readonly IMemoryCache _IMemoryCache;
        private readonly MemoryCacheEntryOptions _MemoryCacheEntryOptions;

        public ProductosController(
            IProductoRepository productoRepository,
            IJWTServices jWTServices,
            IGeneralServices generalServices,
            IMemoryCache memoryCache
        )
        {
            _IProductoRepository = productoRepository;
            _IJWTServices = jWTServices;
            _IGeneralServices = generalServices;
            _IMemoryCache = memoryCache;
            _MemoryCacheEntryOptions = _IGeneralServices.ObtenerMemoryCacheOptions();
        }

        // GET: api/Productos
        [HttpGet]
        [Authorize(Roles = "Administrador,Empleado")]
        public async Task<IActionResult> GetProductos([FromQuery] GetAllParameter getURLParametros)
        {
            if (ModelState.IsValid)
            {
                string limite = getURLParametros.Limite != null ? getURLParametros.Limite.Value.ToString() : "";
                string salto = getURLParametros.Salto != null ? getURLParametros.Salto.Value.ToString() : "";
                var cacheKey = $"GetReport?limite={limite}&salto={salto}";

                if (!_IMemoryCache.TryGetValue(cacheKey, out GetAllResult<Producto>? productos))
                {
                    productos = await _IProductoRepository.ObtenerProductos(getURLParametros);
                    _IMemoryCache.Set(cacheKey, productos, _MemoryCacheEntryOptions);
                }

                ResponseGenericAPI<GetAllResult<Producto>> responseGenericAPI = new()
                {
                    Code = "0000",
                    Message = "Success",
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Data = productos
                };

                return Ok(responseGenericAPI);
            }

            var errors = _IGeneralServices.ModelDetalleErrores(ModelState);

            throw new CustomError((int)HttpStatusCode.BadRequest, null, "", errors);
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Empleado")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {            
            var cacheKey = $"GetProducto/{id}";

            if (!_IMemoryCache.TryGetValue(cacheKey, out Producto? producto))
            {
                producto = await _IProductoRepository.GetByIdAsync(id);
                _IMemoryCache.Set(cacheKey, producto, _MemoryCacheEntryOptions);
            }

            producto = producto ?? throw new CustomError((int)HttpStatusCode.NotFound, "0006", "Datos no encontrados", null);
            ResponseGenericAPI<Producto> responseGenericAPI = new()
            {
                Code = "0000",
                Message = "Success",
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Data = producto
            };

            return Ok(responseGenericAPI);
        }

        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutProducto(int id, ProductoActualizarRequestDto productoActualizarRequestDto)
        {
            if (ModelState.IsValid)
            {
                ResponseGenericAPI<Producto> responseGenericAPI;
                Producto? producto = await _IProductoRepository.GetByIdAsync(productoActualizarRequestDto.ProductoId);
                if ((id != productoActualizarRequestDto.ProductoId) || producto == null)
                {
                    throw new CustomError((int)HttpStatusCode.BadRequest, "0006", "ProductoId requerido no es valido", null);
                }

                string? UsuarioId = _IJWTServices.ObtenerClaimJWT(Request, "nameid");

                producto.ProductoNombre = productoActualizarRequestDto.ProductoNombre;
                producto.ProductoDescripcion = productoActualizarRequestDto.ProductoDescripcion ?? producto.ProductoDescripcion;
                producto.ProductoPrecio = productoActualizarRequestDto.ProductoPrecio ?? producto.ProductoPrecio;
                producto.ProductoCantidad = productoActualizarRequestDto.ProductoCantidad ?? producto.ProductoCantidad;
                producto.ProductoCantidadMinima = productoActualizarRequestDto.ProductoCantidadMinima ?? producto.ProductoCantidadMinima;
                producto.ProveedorId = productoActualizarRequestDto.ProveedoId ?? producto.ProveedorId;
                producto.Estado = productoActualizarRequestDto.Estado ?? producto.Estado;
                producto.ActualizadoPor = UsuarioId;
                producto.FechaActulizado = DateTime.Now;

                _IProductoRepository.Update(producto);

                try
                {
                    await _IProductoRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductoExists(id))
                    {
                        string message = "Datos no encontrados en sistema";
                        responseGenericAPI = new()
                        {
                            Code = "0007",
                            Message = message,
                            TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                            Data = null,
                            Error =
                            [ new() {
                            Field = "General",
                            Message = message
                        }]
                        };

                        return StatusCode((int)HttpStatusCode.NotFound, responseGenericAPI);
                    }
                    else
                    {
                        throw;
                    }
                }

                responseGenericAPI = new()
                {
                    Code = "0000",
                    Message = "Success",
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Data = producto
                };

                return Ok(responseGenericAPI);
            }

            var errors = _IGeneralServices.ModelDetalleErrores(ModelState);

            throw new CustomError((int)HttpStatusCode.BadRequest, null, "", errors);
        }

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Producto>> PostProducto(ProductoCrearRequestDto productoCrearRequestDto)
        {
            if (ModelState.IsValid)
            {
                string? UsuarioId = _IJWTServices.ObtenerClaimJWT(Request, "nameid");

                Producto producto = new()
                {
                    ProductoNombre = productoCrearRequestDto.ProductoNombre,
                    ProductoDescripcion = productoCrearRequestDto.ProductoDescripcion,
                    ProductoPrecio = productoCrearRequestDto.ProductoPrecio,
                    ProductoCantidad = productoCrearRequestDto.ProductoCantidad,
                    ProductoCantidadMinima = productoCrearRequestDto.ProductoCantidadMinima,
                    ProveedorId = productoCrearRequestDto.ProveedoId,
                    Estado = productoCrearRequestDto.Estado ?? true,
                    CreadoPor = UsuarioId!
                };

                await _IProductoRepository.AddAsync(producto);
                await _IProductoRepository.SaveChangesAsync();

                ResponseGenericAPI<Producto> responseGenericAPI = new()
                {
                    Code = "0000",
                    Message = "Success",
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Data = producto
                };

                return StatusCode((int)HttpStatusCode.Created, responseGenericAPI);
            }

            var errors = _IGeneralServices.ModelDetalleErrores(ModelState);

            throw new CustomError((int)HttpStatusCode.BadRequest, null, "", errors);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _IProductoRepository.GetByIdAsync(id) ?? throw new CustomError((int)HttpStatusCode.NotFound, "0006", "Datos no encontrados", null);
            _IProductoRepository.Delete(producto);
            await _IProductoRepository.SaveChangesAsync();

            ResponseGenericAPI<Producto> responseGenericAPI = new()
            {
                Code = "0000",
                Message = "Success",
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return Ok(responseGenericAPI);
        }

        private async Task<bool> ProductoExists(int id)
        {
            return (await _IProductoRepository.GetByIdAsync(id) != null);
        }
    }


}
