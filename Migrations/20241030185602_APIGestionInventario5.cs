using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIGestionInventario.Migrations
{
    /// <inheritdoc />
    public partial class APIGestionInventario5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 1,
                columns: new[] { "FechaCreado", "RolNombre" },
                values: new object[] { new DateTime(2024, 10, 30, 12, 55, 59, 847, DateTimeKind.Local).AddTicks(8106), "Administrador" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 1,
                columns: new[] { "FechaCreado", "RolNombre" },
                values: new object[] { new DateTime(2024, 10, 30, 1, 37, 36, 82, DateTimeKind.Local).AddTicks(360), "Administrador " });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 10, 30, 1, 37, 36, 82, DateTimeKind.Local).AddTicks(377));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "admin01",
                column: "FechaCreado",
                value: new DateTime(2024, 10, 30, 1, 37, 36, 82, DateTimeKind.Local).AddTicks(529));
        }
    }
}
