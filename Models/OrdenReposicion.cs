using System.ComponentModel.DataAnnotations;

namespace APIGestionInventario.Models
{
    public class OrdenReposicion
    {
        [Key]
        public int OrdenReposicionId { get; set; }
        public int ProductoId { get; set; }
        public int ProveedorId { get; set; }
        public int ProductoCantidad { get; set; }
        public int OrdenCompraId { get; set; }
        public int OrdenReposiconEstadoId { get; set; } = 1;
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public string CreadoPor { get; set; } = null!;
        public DateTime? FechaActulizado { get; set; }
        public string? ActualizadoPor { get; set; }
        public DateTime? FechaRealizado { get; set; }

        public virtual Proveedor Proveedores { get; set; } = null!;
        public virtual Producto Productos { get; set; } = null!;
        public virtual OrdenCompra OrdenesCompras { get; set; } = null!;
        public virtual OrdenReposionEstado OrdenReposiconEstados { get; set; } = null!;

    }
}
