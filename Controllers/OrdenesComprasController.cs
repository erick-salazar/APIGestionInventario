using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIGestionInventario.Models;
using System.Net;
using APIGestionInventario.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using APIGestionInventario.Interfaces;
using APIGestionInventario.DTOs.Custom;
using APIGestionInventario.DAL.Repositories;
using APIGestionInventario.DTOs.Response;
using APIGestionInventario.Middleware;
using System.Diagnostics;

namespace APIGestionInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador,Empleado")]
    public class OrdenesComprasController : ControllerBase
    {
        private readonly IOrdenCompraRepository _IOrdenCompraRepository;
        private readonly IJWTServices _IJWTServices;
        private readonly IGeneralServices _IGeneralServices;

        public OrdenesComprasController(
            IOrdenCompraRepository ordenCompraRepository,
            IJWTServices jWTServices,
            IGeneralServices generalServices
        )
        {
            _IOrdenCompraRepository = ordenCompraRepository;
            _IJWTServices = jWTServices;
            _IGeneralServices = generalServices;
        }

        // GET: api/OrdenesCompras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenCompra>>> GetOrdenesCompras([FromQuery] GetAllParameter getURLParametros)
        {
            if (ModelState.IsValid)
            {
                GetAllResult<OrdenCompra> ordenesCompra = await _IOrdenCompraRepository.ObtenerOrdenesCompra(getURLParametros);

                ResponseGenericAPI<GetAllResult<OrdenCompra>> responseGenericAPI = new()
                {
                    Code = "0000",
                    Message = "Success",
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Data = ordenesCompra
                };

                return Ok(responseGenericAPI);
            }

            var errors = _IGeneralServices.ModeDetalleErrores(ModelState);

            throw new CustomError((int)HttpStatusCode.BadRequest, null, "", errors);
        }

        // GET: api/OrdenesCompras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenCompra>> GetOrdenesCompra(int id)
        {
            var ordenCompra = await _IOrdenCompraRepository.GetByIdAsync(id);

            if (ordenCompra == null)
            {
                throw new CustomError((int)HttpStatusCode.NotFound, "0006", "Datos no encontrados", null);
            }

            ResponseGenericAPI<OrdenCompra> responseGenericAPI = new()
            {
                Code = "0000",
                Message = "Success",
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Data = ordenCompra
            };

            return Ok(responseGenericAPI);
        }

        // PUT: api/OrdenesCompras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
                    ordenCompra = await _IOrdenCompraRepository.ActualizarOrdenCompra(ordenCompraActualizarRequestDto, UsuarioId);
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

            var errors = _IGeneralServices.ModeDetalleErrores(ModelState);

            throw new CustomError((int)HttpStatusCode.BadRequest, null, "", errors);
        }

        // POST: api/OrdenesCompras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdenCompra>> PostOrdenesCompra(OrdenCompraCrearRequestDto ordenCompraCrearDto)
        {
            if (ModelState.IsValid)
            {
                string? UsuarioId = _IJWTServices.ObtenerClaimJWT(Request, "nameid");

                OrdenCompra ordenCompra = await _IOrdenCompraRepository.CrearOrdenCompra(ordenCompraCrearDto, UsuarioId);

                ResponseGenericAPI<OrdenCompra> responseGenericAPI = new()
                {
                    Code = "0000",
                    Message = "Success",
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Data = ordenCompra
                };

                return StatusCode((int)HttpStatusCode.Created, responseGenericAPI);
            }

            var errors = _IGeneralServices.ModeDetalleErrores(ModelState);

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

