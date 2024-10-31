using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIGestionInventario.Migrations
{
    /// <inheritdoc />
    public partial class GestionInventario4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductoCantidad",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductoCantidadMinima",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductoDescripcion",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RolId", "ActualizadoPor", "CreadoPor", "Estado", "FechaActulizado", "FechaCreado", "RolNombre" },
                values: new object[,]
                {
                    { 1, null, "admin01", false, null, new DateTime(2024, 10, 30, 1, 33, 50, 727, DateTimeKind.Local).AddTicks(9036), "Administrador " },
                    { 2, null, "admin01", false, null, new DateTime(2024, 10, 30, 1, 33, 50, 727, DateTimeKind.Local).AddTicks(9051), "Empleado" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "ProductoCantidad",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "ProductoCantidadMinima",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "ProductoDescripcion",
                table: "Productos");
        }
    }
}
