using System.ComponentModel.DataAnnotations;

namespace APIGestionInventario.DTOs.Request
{
    public class OrdenCompraCrearRequestDto
    {
        [Required]
        public int ProductoId { get; set; }
        [Required]
        public int ProductoCantidad { get; set; }
    }
}
