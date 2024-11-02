using APIGestionInventario.BAL;
using APIGestionInventario.DTOs.Custom;
using APIGestionInventario.DTOs.Response;
using APIGestionInventario.Interfaces;
using APIGestionInventario.Middleware;
using APIGestionInventario.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Net;

namespace APIGestionInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    [Produces("application/json")]
    public class ReportesController : ControllerBase
    {
        private readonly IProductoRepository _IProductoRepository;
        private readonly IOrdenReposicionRepository _IOrdenReposicionRepository;
        private readonly IGeneralServices _IGeneralServices;
        private readonly IMemoryCache _IMemoryCache;
        private readonly MemoryCacheEntryOptions _MemoryCacheEntryOptions;

        public ReportesController(
            IProductoRepository productoRepository,
            IOrdenReposicionRepository ordenReposicionRepository,
            IGeneralServices generalServices,
            IMemoryCache memoryCache
        )
        {
            _IProductoRepository = productoRepository;
            _IOrdenReposicionRepository = ordenReposicionRepository;
            _IGeneralServices = generalServices;
            _IMemoryCache = memoryCache;
            _MemoryCacheEntryOptions = _IGeneralServices.ObtenerMemoryCacheOptions();
        }

        /// <summary>
        /// Generacion de reportes
        /// </summary>
        /// <response code="200">Reporte generado (success)</response>
        /// <response code="400">Error en parametros de solicitud</response>
        /// <response code="401">Usuario no autorizado</response>
        /// <response code="500">Error de servidor</response>
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status500InternalServerError)]
        [HttpGet("{reporte}")]
        public async Task<ActionResult> GetReport(string reporte, [FromQuery] GetAllParameter getURLParametros)
        {
            if (ModelState.IsValid)
            {
                reporte = reporte.ToLower();

                string limite = getURLParametros.Limite != null ? getURLParametros.Limite.Value.ToString() : "";
                string salto = getURLParametros.Salto != null ? getURLParametros.Salto.Value.ToString() : "";
                var cacheKey = $"GetReport/{reporte}?limite={limite}&salto={salto}";

                switch (reporte)
                {
                    case "productos":

                        if (!_IMemoryCache.TryGetValue(cacheKey, out GetAllResult<Producto>? productos))
                        {
                            productos = await _IProductoRepository.ObtenerProductosCantidadMinima(getURLParametros);                                                                              
                            _IMemoryCache.Set(cacheKey, productos, _MemoryCacheEntryOptions);
                        }

                        ResponseGenericAPI<GetAllResult<Producto>> responseProductos = new()
                        {
                            Code = "0000",
                            Message = "Success",
                            TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                            Data = productos
                        };

                        return Ok(responseProductos);

                    case "ordenesreposicion":
                        if (!_IMemoryCache.TryGetValue(cacheKey, out GetAllResult<OrdenReposicion>? ordenReposicion))
                        {
                            ordenReposicion = await _IOrdenReposicionRepository.ObtenerOrdenReposicion(getURLParametros);
                            _IMemoryCache.Set(cacheKey, ordenReposicion, _MemoryCacheEntryOptions);
                        }

                        ResponseGenericAPI<GetAllResult<OrdenReposicion>> responseordenesReposicion = new()
                        {
                            Code = "0000",
                            Message = "Success",
                            TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                            Data = ordenReposicion
                        };

                        return Ok(responseordenesReposicion);
                    default:
                        string message = "Reporte no valido";

                        ResponseGenericAPI<GetAllResult<Producto>> responseGenericAPI = new()
                        {
                            Code = "0006",
                            Message = message,
                            TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                            Error =
                            [ new() {
                            Field = "General",
                            Message = message
                            }]
                        };

                        return BadRequest(responseGenericAPI);
                }
            }

            var errors = _IGeneralServices.ModelDetalleErrores(ModelState);

            throw new CustomError((int)HttpStatusCode.BadRequest, null, "", errors);
        }
    }
}
