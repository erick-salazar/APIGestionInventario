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
            
            modelBuilder.Entity<OrdenesCompra>()
                .HasOne(d => d.Productos).WithMany(p => p.OrdenesCompras)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Usuario>()
                .HasOne(p => p.Roles)
                .WithMany(a => a.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Rol>().HasData(
                new Rol { RolId=1, RolNombre = "Administrador", CreadoPor = "admin01" },
                new Rol { RolId=2, RolNombre = "Empleado", CreadoPor = "admin01" }
            );

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { UsuarioId="admin01", RolId=1,Password = "@1234Abc", CreadoPor= "admin01", UsuarioNombre="Admin", UsuarioApellido="System", Estado=true },
                new Usuario { UsuarioId = "empleado01", RolId = 2, Password = "123Abc+", CreadoPor = "admin01", UsuarioNombre = "Juan", UsuarioApellido = "Perez", Estado=true }
            );
        }

        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<OrdenesCompra> OrdenesCompras { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
    }
}
