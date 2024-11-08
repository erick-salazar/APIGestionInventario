﻿using Microsoft.AspNetCore.Mvc;
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
    [Produces("application/json")]
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

        /// <summary>
        /// Listado de productos
        /// </summary>
        /// <response code="200">Lista de productos (success)</response>
        /// <response code="400">Error en parametros de solicitud</response>
        /// <response code="401">Usuario no autorizado</response>
        /// <response code="500">Error de servidor</response>
        [HttpGet]
        [Authorize(Roles = "Administrador,Empleado")]
        [ProducesResponseType(typeof(ResponseGenericAPI<GetAllResult<Producto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductos([FromQuery] GetAllParameter getURLParametros)
        {
            if (ModelState.IsValid)
            {
                string limite = getURLParametros.Limite != null ? getURLParametros.Limite.Value.ToString() : "";
                string salto = getURLParametros.Salto != null ? getURLParametros.Salto.Value.ToString() : "";
                var cacheKey = $"GetProductos?limite={limite}&salto={salto}";

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


        /// <summary>
        /// Informacion de producto
        /// </summary>
        /// <response code="200">Informacion de producto (success)</response>
        /// <response code="400">Error en parametros de solicitud</response>
        /// <response code="401">Usuario no autorizado</response>
        /// <response code="404">Producto no encotrado</response>
        /// <response code="500">Error de servidor</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Empleado")]
        [ProducesResponseType(typeof(ResponseGenericAPI<Producto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Actualizar producto
        /// </summary>
        /// <response code="200">Producto actualizado (success)</response>
        /// <response code="400">Error en parametros de solicitud</response>
        /// <response code="401">Usuario no autorizado</response>
        /// <response code="404">Producto no encotrado</response>
        /// <response code="500">Error de servidor</response>
        [ProducesResponseType(typeof(ResponseGenericAPI<Producto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status500InternalServerError)]
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


        /// <summary>
        /// Crear producto
        /// </summary>
        /// <response code="201">Producto agregado (success)</response>
        /// <response code="400">Error en parametros de solicitud</response>
        /// <response code="401">Usuario no autorizado</response>
        /// <response code="404">Producto no encotrado</response>
        /// <response code="500">Error de servidor</response>
        [ProducesResponseType(typeof(ResponseGenericAPI<Producto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status500InternalServerError)]
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
                    ProveedorId = productoCrearRequestDto.ProveedorId,
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

        /// <summary>
        /// Eliminar producto
        /// </summary>
        /// <response code="200">Producto eliminado (success)</response>
        /// <response code="401">Usuario no autorizado</response>
        /// <response code="404">Producto no encotrado</response>
        /// <response code="500">Error de servidor</response>
        [ProducesResponseType(typeof(ResponseGenericAPI<OrdenCompra>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status500InternalServerError)]
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
