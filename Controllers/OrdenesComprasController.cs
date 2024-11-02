using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIGestionInventario.Models;
using System.Net;
using APIGestionInventario.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using APIGestionInventario.Interfaces;
using APIGestionInventario.DTOs.Custom;
using APIGestionInventario.DTOs.Response;
using APIGestionInventario.Middleware;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace APIGestionInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador,Empleado")]
    [Produces("application/json")]
    public class OrdenesComprasController : ControllerBase
    {
        private readonly IOrdenCompraRepository _IOrdenCompraRepository;
        private readonly IJWTServices _IJWTServices;
        private readonly IGeneralServices _IGeneralServices;
        private readonly IMemoryCache _IMemoryCache;
        private readonly MemoryCacheEntryOptions _MemoryCacheEntryOptions;

        public OrdenesComprasController(
            IOrdenCompraRepository ordenCompraRepository,
            IJWTServices jWTServices,
            IGeneralServices generalServices,
            IMemoryCache memoryCache
        )
        {
            _IOrdenCompraRepository = ordenCompraRepository;
            _IJWTServices = jWTServices;
            _IGeneralServices = generalServices;
            _IMemoryCache = memoryCache;
            _MemoryCacheEntryOptions = _IGeneralServices.ObtenerMemoryCacheOptions();
        }

        /// <summary>
        /// Listado de ordenes de compras
        /// </summary>
        /// <response code="200">Listado de ordenes de compras</response>
        /// <response code="400">Error en parametros de solicitud</response>
        /// <response code="401">Usuario no autorizado</response>
        /// <response code="500">Error de servidor</response>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseGenericAPI<GetAllResult<OrdenCompra>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseGenericAPI<GetAllResult<object>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseGenericAPI<GetAllResult<object>>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<OrdenCompra>>> GetOrdenesCompras([FromQuery] GetAllParameter getURLParametros)
        {
            if (ModelState.IsValid)
            {
                string limite = getURLParametros.Limite != null ? getURLParametros.Limite.Value.ToString() : "";
                string salto = getURLParametros.Salto != null ? getURLParametros.Salto.Value.ToString() : "";
                var cacheKey = $"GetOrdenesCompras?limite={limite}&salto={salto}";

                if (!_IMemoryCache.TryGetValue(cacheKey, out GetAllResult<OrdenCompra>? ordenesCompra))
                {
                    ordenesCompra =  await _IOrdenCompraRepository.ObtenerOrdenesCompra(getURLParametros);
                    _IMemoryCache.Set(cacheKey, ordenesCompra, _MemoryCacheEntryOptions);
                }                  

                ResponseGenericAPI<GetAllResult<OrdenCompra>> responseGenericAPI = new()
                {
                    Code = "0000",
                    Message = "Success",
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Data = ordenesCompra
                };

                return Ok(responseGenericAPI);
            }

            var errors = _IGeneralServices.ModelDetalleErrores(ModelState);

            throw new CustomError((int)HttpStatusCode.BadRequest, null, "", errors);
        }

        /// <summary>
        /// Informacion orden de compra
        /// </summary>
        /// <response code="200">Informacion orden de compra (success)</response>
        /// <response code="400">Error en parametros de solicitud</response>
        /// <response code="401">Usuario no autorizado</response>
        /// <response code="404">Orden no encotrada</response>
        /// <response code="500">Error de servidor</response>
        [ProducesResponseType(typeof(ResponseGenericAPI<OrdenCompra>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenCompra>> GetOrdenesCompra(int id)
        {
            var cacheKey = $"GetOrdenesCompra/{id}";

            if (!_IMemoryCache.TryGetValue(cacheKey, out OrdenCompra? ordenCompra))
            {
                ordenCompra = await _IOrdenCompraRepository.GetByIdAsync(id);
                _IMemoryCache.Set(cacheKey, ordenCompra, _MemoryCacheEntryOptions);
            }

            ordenCompra = ordenCompra ?? throw new CustomError((int)HttpStatusCode.NotFound, "0006", "Datos no encontrados", null);
            ResponseGenericAPI<OrdenCompra> responseGenericAPI = new()
            {
                Code = "0000",
                Message = "Success",
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Data = ordenCompra
            };

            return Ok(responseGenericAPI);
        }

        /// <summary>
        /// Actualizar orden de compra
        /// </summary>
        /// <response code="200">orden de compra actualizada (success)</response>
        /// <response code="400">Error en parametros de solicitud</response>
        /// <response code="401">Usuario no autorizado</response>
        /// <response code="404">Orden no encotrada</response>
        /// <response code="500">Error de servidor</response>
        [ProducesResponseType(typeof(ResponseGenericAPI<OrdenCompra>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdenesCompra(int id, OrdenCompraActualizarRequestDto ordenCompraActualizarRequestDto)
        {
            if (ModelState.IsValid)
            {
                ResponseGenericAPI<OrdenCompra> responseGenericAPI;
                OrdenCompra? ordenCompra = await _IOrdenCompraRepository.GetByOrdenProductoIdAsync(ordenCompraActualizarRequestDto.OrdenCompraId, ordenCompraActualizarRequestDto.ProductoId);
                if ((id != ordenCompraActualizarRequestDto.OrdenCompraId) || ordenCompra == null)
                {
                    throw new CustomError((int)HttpStatusCode.BadRequest, "0006", "OrdenCompraId y productoId requerido no es valido", null);
                }

                string? UsuarioId = _IJWTServices.ObtenerClaimJWT(Request, "nameid");
                
                try
                {
                    ordenCompra = await _IOrdenCompraRepository.ActualizarOrdenCompra(ordenCompraActualizarRequestDto, UsuarioId!);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OrdenesCompraExists(id))
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
                    Data = ordenCompra
                };

                return Ok(responseGenericAPI);
            }

            var errors = _IGeneralServices.ModelDetalleErrores(ModelState);

            throw new CustomError((int)HttpStatusCode.BadRequest, null, "", errors);
        }

        /// <summary>
        /// Crear orden de compra
        /// </summary>
        /// <response code="201">orden de compra creada (success)</response>
        /// <response code="400">Error en parametros de solicitud</response>
        /// <response code="401">Usuario no autorizado</response>
        /// <response code="500">Error de servidor</response>
        [ProducesResponseType(typeof(ResponseGenericAPI<OrdenCompra>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<OrdenCompra>> PostOrdenesCompra(OrdenCompraCrearRequestDto ordenCompraCrearDto)
        {
            if (ModelState.IsValid)
            {
                string? UsuarioId = _IJWTServices.ObtenerClaimJWT(Request, "nameid");

                OrdenCompra ordenCompra = await _IOrdenCompraRepository.CrearOrdenCompra(ordenCompraCrearDto, UsuarioId!);

                ResponseGenericAPI<OrdenCompra> responseGenericAPI = new()
                {
                    Code = "0000",
                    Message = "Success",
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Data = ordenCompra
                };

                return StatusCode((int)HttpStatusCode.Created, responseGenericAPI);
            }

            var errors = _IGeneralServices.ModelDetalleErrores(ModelState);

            throw new CustomError((int)HttpStatusCode.BadRequest, null, "", errors);
        }

        /*
        // DELETE: api/OrdenesCompras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdenesCompra(int id)
        {
            var ordenesCompra = await _IRepositoyOrdenesCompra.OrdenesCompras.FindAsync(id);
            if (ordenesCompra == null)
            {
                return NotFound();
            }

            _IRepositoyOrdenesCompra.OrdenesCompras.Remove(ordenesCompra);
            await _IRepositoyOrdenesCompra.SaveChangesAsync();

            return NoContent();
        }
        */

        private async Task<bool> OrdenesCompraExists(int id)
        {
            return (await _IOrdenCompraRepository.GetByIdAsync(id) != null);
        }
        
    }
}

