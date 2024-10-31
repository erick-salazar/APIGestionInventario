using APIGestionInventario.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIGestionInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class ReportesController : ControllerBase
    {
        private readonly GestionInventarioContext _context;

        public ReportesController(GestionInventarioContext context)
        {
            _context = context;
        }

        [HttpGet("{reporte}")]
        public async Task<ActionResult> GetReport(string reporte)
        {
            try
            {
                switch (reporte.ToLower())
                {
                    case "productos":
                        var productos = await ReportesGenerales<Producto>("productos");
                        return Ok(productos); 
                    default:
                        return BadRequest("Reporte no válido");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        public async Task<List<T>> ReportesGenerales<T>(string reporte) where T : class
        {
            try
            {
                if (reporte == "productos" && typeof(T) == typeof(Producto))
                {
                    return await _context.Productos
                        .Where(x => x.ProductoCantidad <= x.ProductoCantidadMinima)
                        .Cast<T>()
                        .ToListAsync();
                }                

                throw new ArgumentException("Tipo de reporte no válido o tipo T incorrecto.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error ejecutando la consulta.", ex);
            }
        }

    }
}
