using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIGestionInventario.Migrations
{
    /// <inheritdoc />
    public partial class APIGestionInventario6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 10, 30, 13, 0, 8, 65, DateTimeKind.Local).AddTicks(9879));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 10, 30, 13, 0, 8, 65, DateTimeKind.Local).AddTicks(9901));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "admin01",
                column: "FechaCreado",
                value: new DateTime(2024, 10, 30, 13, 0, 8, 66, DateTimeKind.Local).AddTicks(216));

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "ActualizadoPor", "CreadoPor", "Estado", "FechaActulizado", "FechaCreado", "Password", "RolId", "UsuarioApellido", "UsuarioNombre" },
                values: new object[] { "empleado01", null, "admin01", false, null, new DateTime(2024, 10, 30, 13, 0, 8, 66, DateTimeKind.Local).AddTicks(225), "123Abc+", 1, "Perez", "Juan" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "empleado01");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 10, 30, 12, 55, 59, 847, DateTimeKind.Local).AddTicks(8106));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 10, 30, 12, 55, 59, 847, DateTimeKind.Local).AddTicks(8127));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "admin01",
                column: "FechaCreado",
                value: new DateTime(2024, 10, 30, 12, 55, 59, 847, DateTimeKind.Local).AddTicks(8497));
        }
    }
}
