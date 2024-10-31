﻿// <auto-generated />
using System;
using APIGestionInventario.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APIGestionInventario.Migrations
{
    [DbContext(typeof(GestionInventarioContext))]
    partial class GestionInventarioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APIGestionInventario.Models.OrdenesCompra", b =>
                {
                    b.Property<int>("OrdenCompraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrdenCompraId"));

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaActulizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreado")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductoCantidad")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<decimal>("ProductoPrecio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrdenCompraId");

                    b.HasIndex("ProductoId");

                    b.ToTable("OrdenesCompras");
                });

            modelBuilder.Entity("APIGestionInventario.Models.Producto", b =>
                {
                    b.Property<int>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductoId"));

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaActulizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreado")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductoCantidad")
                        .HasColumnType("int");

                    b.Property<int>("ProductoCantidadMinima")
                        .HasColumnType("int");

                    b.Property<string>("ProductoDescripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductoNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProductoPrecio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductoId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("APIGestionInventario.Models.Rol", b =>
                {
                    b.Property<int>("RolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RolId"));

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaActulizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreado")
                        .HasColumnType("datetime2");

                    b.Property<string>("RolNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RolId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RolId = 1,
                            CreadoPor = "admin01",
                            Estado = false,
                            FechaCreado = new DateTime(2024, 10, 31, 14, 38, 39, 0, DateTimeKind.Local).AddTicks(6590),
                            RolNombre = "Administrador"
                        },
                        new
                        {
                            RolId = 2,
                            CreadoPor = "admin01",
                            Estado = false,
                            FechaCreado = new DateTime(2024, 10, 31, 14, 38, 39, 0, DateTimeKind.Local).AddTicks(6613),
                            RolNombre = "Empleado"
                        });
                });

            modelBuilder.Entity("APIGestionInventario.Models.Usuario", b =>
                {
                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaActulizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreado")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioApellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsuarioId");

                    b.HasIndex("RolId");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            UsuarioId = "admin01",
                            CreadoPor = "admin01",
                            Estado = true,
                            FechaCreado = new DateTime(2024, 10, 31, 14, 38, 39, 0, DateTimeKind.Local).AddTicks(7027),
                            Password = "@1234Abc",
                            RolId = 1,
                            UsuarioApellido = "System",
                            UsuarioNombre = "Admin"
                        },
                        new
                        {
                            UsuarioId = "empleado01",
                            CreadoPor = "admin01",
                            Estado = true,
                            FechaCreado = new DateTime(2024, 10, 31, 14, 38, 39, 0, DateTimeKind.Local).AddTicks(7036),
                            Password = "123Abc+",
                            RolId = 2,
                            UsuarioApellido = "Perez",
                            UsuarioNombre = "Juan"
                        });
                });

            modelBuilder.Entity("APIGestionInventario.Models.OrdenesCompra", b =>
                {
                    b.HasOne("APIGestionInventario.Models.Producto", "Productos")
                        .WithMany("OrdenesCompras")
                        .HasForeignKey("ProductoId")
                        .IsRequired();

                    b.Navigation("Productos");
                });

            modelBuilder.Entity("APIGestionInventario.Models.Usuario", b =>
                {
                    b.HasOne("APIGestionInventario.Models.Rol", "Roles")
                        .WithMany("Usuarios")
                        .HasForeignKey("RolId")
                        .IsRequired();

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("APIGestionInventario.Models.Producto", b =>
                {
                    b.Navigation("OrdenesCompras");
                });

            modelBuilder.Entity("APIGestionInventario.Models.Rol", b =>
                {
                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
