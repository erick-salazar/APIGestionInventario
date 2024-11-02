using Microsoft.EntityFrameworkCore;
using System;

namespace APIGestionInventario.Models
{
    public class GestionInventarioContext : DbContext
    {
        public GestionInventarioContext(DbContextOptions<GestionInventarioContext> options)
       : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder.Entity<OrdenCompra>()
                .HasOne(d => d.Productos).WithMany(p => p.OrdenesCompras)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Usuario>()
                .HasOne(p => p.Roles)
                .WithMany(a => a.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Proveedores)
                .WithMany(a => a.Productos)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<OrdenReposicion>()
                .HasOne(p => p.Proveedores)
                .WithMany(a => a.OrdenesReposiciones)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<OrdenReposicion>()
               .HasOne(p => p.Productos)
               .WithMany(a => a.OrdenesReposiciones)
               .HasForeignKey(d => d.ProductoId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<OrdenReposicion>()
               .HasOne(p => p.OrdenesCompras)
               .WithMany(a => a.OrdenesReposiciones)
               .HasForeignKey(d => d.OrdenCompraId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<OrdenReposicion>()
               .HasOne(p => p.OrdenReposiconEstados)
               .WithMany(a => a.OrdenesReposiciones)
               .HasForeignKey(d => d.OrdenReposiconEstadoId)
               .OnDelete(DeleteBehavior.ClientSetNull);
                        
            modelBuilder.Entity<Rol>().HasData(
                new Rol { RolId=1, RolNombre = "Administrador", CreadoPor = "admin01" },
                new Rol { RolId=2, RolNombre = "Empleado", CreadoPor = "admin01" }
            );

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { UsuarioId="admin01", RolId=1,Password = "@1234Abc", CreadoPor= "admin01", UsuarioNombre="Admin", UsuarioApellido="System", Estado=true },
                new Usuario { UsuarioId = "empleado01", RolId = 2, Password = "123Abc+", CreadoPor = "admin01", UsuarioNombre = "Juan", UsuarioApellido = "Perez", Estado=true }
            );

            modelBuilder.Entity<Proveedor>().HasData(
                new Proveedor { ProveedorId = 1, ProveedorNombre = "Dell", CreadoPor = "admin01", Estado = true },
                new Proveedor { ProveedorId = 2, ProveedorNombre="Lenovo", CreadoPor = "admin01", Estado = true }
            );

            modelBuilder.Entity<Producto>().HasData(
               new Producto { ProductoId=1, ProveedorId = 1, ProductoNombre = "Dell Latitude", ProductoDescripcion="I7 Dell", ProductoCantidad=10, ProductoCantidadMinima=2, CreadoPor = "admin01", Estado = true },
               new Producto { ProductoId=2, ProveedorId = 2, ProductoNombre = "Lenovo ThinkPad", ProductoDescripcion="I7 Lenovo", ProductoCantidad = 10, ProductoCantidadMinima = 2, CreadoPor = "admin01", Estado = true }
            );

            modelBuilder.Entity<OrdenReposionEstado>().HasData(
                new OrdenReposionEstado { OrdenReposicionEstadoId = 1, EstadoNombre = "Pendiente", CreadoPor = "admin01" },
                new OrdenReposionEstado { OrdenReposicionEstadoId = 3, EstadoNombre = "Cancelado", CreadoPor = "admin01" },
                new OrdenReposionEstado { OrdenReposicionEstadoId = 2, EstadoNombre = "Realizado", CreadoPor = "admin01" }
            );
        }

        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<OrdenCompra> OrdenesCompras { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
        public virtual DbSet<Proveedor> Proveedores { get; set; }
        public virtual DbSet<OrdenReposicion> OrdenesReposiciones { get; set; }
        public virtual DbSet<OrdenReposionEstado> OrdenReposionEstados { get; set; }
    }
}
