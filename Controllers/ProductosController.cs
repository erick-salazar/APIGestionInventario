using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIGestionInventario.Models;
using Microsoft.AspNetCore.Authorization;
using APIGestionInventario.Interfaces;

namespace APIGestionInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IRepositoyGestionInventarioDB<Producto> _IRepositoyProducto;

        public ProductosController(IRepositoyGestionInventarioDB<Producto> iRepositoyProducto)
        {
            _IRepositoyProducto = iRepositoyProducto;
        }
        
        // GET: api/Productos
        [HttpGet]
        [Authorize(Roles = "Administrador,Empleado")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return Ok(await _IRepositoyProducto.GetAllAsync());
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Empleado")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _IRepositoyProducto.GetByIdAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }
        
        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return BadRequest();
            }

            _IRepositoyProducto.Update(producto);

            try
            {
                await _IRepositoyProducto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await ProductoExists(id))
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

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            await _IRepositoyProducto.AddAsync(producto);
            await _IRepositoyProducto.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.ProductoId }, producto);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _IRepositoyProducto.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _IRepositoyProducto.Delete(producto);
            await _IRepositoyProducto.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ProductoExists(int id)
        {
            return (await _IRepositoyProducto.GetByIdAsync(id) !=null);
        }
    }
}
