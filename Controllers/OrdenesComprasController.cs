using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIGestionInventario.Models;
using System.Net;
using APIGestionInventario.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using APIGestionInventario.Interfaces;
using APIGestionInventario.DAL.Repositories;

namespace APIGestionInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador,Empleado")]
    public class OrdenesComprasController : ControllerBase
    {
        private readonly IRepositoyGestionInventarioDB<OrdenesCompra> _IRepositoyOrdenesCompra;
        private readonly IRepositoyGestionInventarioDB<Producto> _IRepositoyProducto;

        public OrdenesComprasController(
            IRepositoyGestionInventarioDB<OrdenesCompra> repositoyOrdenesCompra,
            IRepositoyGestionInventarioDB<Producto> iRepositoyProducto)
        {
            _IRepositoyOrdenesCompra = repositoyOrdenesCompra;
            _IRepositoyProducto = iRepositoyProducto;
        }

        // GET: api/OrdenesCompras
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<OrdenesCompra>>> GetOrdenesCompras()
        {
            return Ok(await _IRepositoyOrdenesCompra.GetAllAsync());
        }

        // GET: api/OrdenesCompras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenesCompra>> GetOrdenesCompra(int id)
        {
            var ordenesCompra = await _IRepositoyOrdenesCompra.GetByIdAsync(id);

            if (ordenesCompra == null)
            {
                return NotFound();
            }

            return ordenesCompra;
        }

        // PUT: api/OrdenesCompras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdenesCompra(int id, OrdenesCompra ordenesCompra)
        {
            if (id != ordenesCompra.OrdenCompraId)
            {
                return BadRequest();
            }

            _IRepositoyOrdenesCompra.Update(ordenesCompra);

            try
            {
                await _IRepositoyOrdenesCompra.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrdenesCompraExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrdenesCompras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdenesCompra>> PostOrdenesCompra(CrearOrdenCompraRequestDto crearOrdenCompraDto)
        {

            Producto? producto = await _IRepositoyProducto.GetByIdAsync(crearOrdenCompraDto.ProductoId);

            if (producto != null)
            {
                OrdenesCompra crearOrdenCompra = new()
                {
                    ProductoId = crearOrdenCompraDto.ProductoId,
                    ProductoCantidad = crearOrdenCompraDto.ProductoCantidad,
                    ProductoPrecio = producto.ProductoPrecio,
                    CreadoPor = "Test"
                };

                //using var transaction = _IRepositoyOrdenesCompra.Database.BeginTransaction();

                producto.ProductoCantidad -= crearOrdenCompra.ProductoCantidad;
                _IRepositoyProducto.Update(producto);
                await _IRepositoyProducto.SaveChangesAsync();

                await _IRepositoyOrdenesCompra.AddAsync(crearOrdenCompra);
                await _IRepositoyOrdenesCompra.SaveChangesAsync();

                return CreatedAtAction("GetOrdenesCompra", new { id = crearOrdenCompra.OrdenCompraId }, crearOrdenCompra);

            }
            return StatusCode((int)HttpStatusCode.NotFound);
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
            return (await _IRepositoyOrdenesCompra.GetByIdAsync(id) != null);
        }
    }
}
            
