﻿// <auto-generated />
using System;
using APIGestionInventario.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APIGestionInventario.Migrations
{
    [DbContext(typeof(GestionInventarioContext))]
    [Migration("20241101222820_APIGestionInventario13")]
    partial class APIGestionInventario13
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APIGestionInventario.Models.OrdenCompra", b =>
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

            modelBuilder.Entity("APIGestionInventario.Models.OrdenReposicion", b =>
                {
                    b.Property<int>("OrdenReposicionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrdenReposicionId"));

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaActulizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaRealizado")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrdenCompraId")
                        .HasColumnType("int");

                    b.Property<int>("OrdenReposiconEstadoId")
                        .HasColumnType("int");

                    b.Property<int>("ProductoCantidad")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("ProveedorId")
                        .HasColumnType("int");

                    b.HasKey("OrdenReposicionId");

                    b.HasIndex("OrdenCompraId");

                    b.HasIndex("OrdenReposiconEstadoId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("ProveedorId");

                    b.ToTable("OrdenesReposiciones");
                });

            modelBuilder.Entity("APIGestionInventario.Models.OrdenReposionEstado", b =>
                {
                    b.Property<int>("OrdenReposicionEstadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrdenReposicionEstadoId"));

                    b.Property<string>("ActualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("EstadoNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaActulizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreado")
                        .HasColumnType("datetime2");

                    b.HasKey("OrdenReposicionEstadoId");

                    b.ToTable("OrdenReposionEstado");

                    b.HasData(
                        new
                        {
                            OrdenReposicionEstadoId = 1,
                            CreadoPor = "admin01",
                            Estado = true,
                            EstadoNombre = "Pendiente",
                            FechaCreado = new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7613)
                        },
                        new
                        {
                            OrdenReposicionEstadoId = 3,
                            CreadoPor = "admin01",
                            Estado = true,
                            EstadoNombre = "Cancelado",
                            FechaCreado = new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7618)
                        },
                        new
                        {
                            OrdenReposicionEstadoId = 2,
                            CreadoPor = "admin01",
                            Estado = true,
                            EstadoNombre = "Realizado",
                            FechaCreado = new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7620)
                        });
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

                    b.Property<int>("ProveedorId")
                        .HasColumnType("int");

                    b.HasKey("ProductoId");

                    b.HasIndex("ProveedorId");

                    b.ToTable("Productos");

                    b.HasData(
                        new
                        {
                            ProductoId = 1,
                            CreadoPor = "admin01",
                            Estado = true,
                            FechaCreado = new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7543),
                            ProductoCantidad = 10,
                            ProductoCantidadMinima = 2,
                            ProductoDescripcion = "I7 Dell",
                            ProductoNombre = "Dell Latitude",
                            ProductoPrecio = 0m,
                            ProveedorId = 1
                        },
                        new
                        {
                            ProductoId = 2,
                            CreadoPor = "admin01",
                            Estado = true,
                            FechaCreado = new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7552),
                            ProductoCantidad = 10,
                            ProductoCantidadMinima = 2,
                            ProductoDescripcion = "I7 Lenovo",
                            ProductoNombre = "Lenovo ThinkPad",
                            ProductoPrecio = 0m,
                            ProveedorId = 2
                        });
                });

            modelBuilder.Entity("APIGestionInventario.Models.Proveedor", b =>
                {
                    b.Property<int>("ProveedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProveedorId"));

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

                    b.Property<string>("ProveedorNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProveedorId");

                    b.ToTable("Proveedores");

                    b.HasData(
                        new
                        {
                            ProveedorId = 1,
                            CreadoPor = "admin01",
                            Estado = true,
                            FechaCreado = new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7479),
                            ProveedorNombre = "Dell"
                        },
                        new
                        {
                            ProveedorId = 2,
                            CreadoPor = "admin01",
                            Estado = true,
                            FechaCreado = new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7492),
                            ProveedorNombre = "Lenovo"
                        });
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
                            FechaCreado = new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7037),
                            RolNombre = "Administrador"
                        },
                        new
                        {
                            RolId = 2,
                            CreadoPor = "admin01",
                            Estado = false,
                            FechaCreado = new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7057),
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
                            FechaCreado = new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7424),
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
                            FechaCreado = new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7429),
                            Password = "123Abc+",
                            RolId = 2,
                            UsuarioApellido = "Perez",
                            UsuarioNombre = "Juan"
                        });
                });

            modelBuilder.Entity("APIGestionInventario.Models.OrdenCompra", b =>
                {
                    b.HasOne("APIGestionInventario.Models.Producto", "Productos")
                        .WithMany("OrdenesCompras")
                        .HasForeignKey("ProductoId")
                        .IsRequired();

                    b.Navigation("Productos");
                });

            modelBuilder.Entity("APIGestionInventario.Models.OrdenReposicion", b =>
                {
                    b.HasOne("APIGestionInventario.Models.OrdenCompra", "OrdenesCompras")
                        .WithMany("OrdenesReposiciones")
                        .HasForeignKey("OrdenCompraId")
                        .IsRequired();

                    b.HasOne("APIGestionInventario.Models.OrdenReposionEstado", "OrdenReposiconEstados")
                        .WithMany("OrdenesReposiciones")
                        .HasForeignKey("OrdenReposiconEstadoId")
                        .IsRequired();

                    b.HasOne("APIGestionInventario.Models.Producto", "Productos")
                        .WithMany("OrdenesReposiciones")
                        .HasForeignKey("ProductoId")
                        .IsRequired();

                    b.HasOne("APIGestionInventario.Models.Proveedor", "Proveedores")
                        .WithMany("OrdenesReposiciones")
                        .HasForeignKey("ProveedorId")
                        .IsRequired();

                    b.Navigation("OrdenReposiconEstados");

                    b.Navigation("OrdenesCompras");

                    b.Navigation("Productos");

                    b.Navigation("Proveedores");
                });

            modelBuilder.Entity("APIGestionInventario.Models.Producto", b =>
                {
                    b.HasOne("APIGestionInventario.Models.Proveedor", "Proveedores")
                        .WithMany("Productos")
                        .HasForeignKey("ProveedorId")
                        .IsRequired();

                    b.Navigation("Proveedores");
                });

            modelBuilder.Entity("APIGestionInventario.Models.Usuario", b =>
                {
                    b.HasOne("APIGestionInventario.Models.Rol", "Roles")
                        .WithMany("Usuarios")
                        .HasForeignKey("RolId")
                        .IsRequired();

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("APIGestionInventario.Models.OrdenCompra", b =>
                {
                    b.Navigation("OrdenesReposiciones");
                });

            modelBuilder.Entity("APIGestionInventario.Models.OrdenReposionEstado", b =>
                {
                    b.Navigation("OrdenesReposiciones");
                });

            modelBuilder.Entity("APIGestionInventario.Models.Producto", b =>
                {
                    b.Navigation("OrdenesCompras");

                    b.Navigation("OrdenesReposiciones");
                });

            modelBuilder.Entity("APIGestionInventario.Models.Proveedor", b =>
                {
                    b.Navigation("OrdenesReposiciones");

                    b.Navigation("Productos");
                });

            modelBuilder.Entity("APIGestionInventario.Models.Rol", b =>
                {
                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}