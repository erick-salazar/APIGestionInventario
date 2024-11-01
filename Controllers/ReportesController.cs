using APIGestionInventario.BAL;
using APIGestionInventario.DTOs.Custom;
using APIGestionInventario.DTOs.Response;
using APIGestionInventario.Interfaces;
using APIGestionInventario.Middleware;
using APIGestionInventario.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;

namespace APIGestionInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class ReportesController : ControllerBase
    {
        private readonly IProductoRepository _IProductoRepository;
        private readonly IOrdenReposicionRepository _IOrdenReposicionRepository;
        private readonly IGeneralServices _IGeneralServices;

        public ReportesController(
            IProductoRepository productoRepository,
            IOrdenReposicionRepository ordenReposicionRepository,
            IGeneralServices generalServices
        )
        {
            _IProductoRepository = productoRepository;
            _IOrdenReposicionRepository = ordenReposicionRepository;
            _IGeneralServices = generalServices;
        }

        [HttpGet("{reporte}")]
        public async Task<ActionResult> GetReport(string reporte, [FromQuery] GetAllParameter getURLParametros)
        {

            if (ModelState.IsValid)
            {
                reporte = reporte.ToLower();
                switch (reporte)
                {
                    case "productos":

                        var productos = await _IProductoRepository.ObtenerProductosCantidadMinima(getURLParametros);

                        ResponseGenericAPI<GetAllResult<Producto>> responseProductos = new()
                        {
                            Code = "0000",
                            Message = "Success",
                            TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                            Data = productos
                        };

                        return Ok(responseProductos);

                    case "ordenesReposicion":
                        var ordenReposicion = await _IOrdenReposicionRepository.ObtenerOrdenReposicion(getURLParametros);

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

            var errors = _IGeneralServices.ModeDetalleErrores(ModelState);

            throw new CustomError((int)HttpStatusCode.BadRequest, null, "", errors);
        }
                
    }
}
