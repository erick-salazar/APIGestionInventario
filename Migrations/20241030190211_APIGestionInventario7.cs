using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIGestionInventario.Migrations
{
    /// <inheritdoc />
    public partial class APIGestionInventario7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 10, 30, 13, 2, 9, 604, DateTimeKind.Local).AddTicks(4640));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 10, 30, 13, 2, 9, 604, DateTimeKind.Local).AddTicks(4654));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "admin01",
                column: "FechaCreado",
                value: new DateTime(2024, 10, 30, 13, 2, 9, 604, DateTimeKind.Local).AddTicks(4885));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "empleado01",
                columns: new[] { "FechaCreado", "RolId" },
                values: new object[] { new DateTime(2024, 10, 30, 13, 2, 9, 604, DateTimeKind.Local).AddTicks(4889), 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "empleado01",
                columns: new[] { "FechaCreado", "RolId" },
                values: new object[] { new DateTime(2024, 10, 30, 13, 0, 8, 66, DateTimeKind.Local).AddTicks(225), 1 });
        }
    }
}
