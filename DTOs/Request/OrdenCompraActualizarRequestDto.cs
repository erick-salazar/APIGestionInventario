using Microsoft.Build.Framework;

namespace APIGestionInventario.DTOs.Request
{
    public class OrdenCompraActualizarRequestDto
    {
        [Required]
        public int OrdenCompraId { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required]
        public int ProductoCantidad { get; set; }
    }
}
