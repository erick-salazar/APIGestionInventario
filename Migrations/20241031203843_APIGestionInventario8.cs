using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIGestionInventario.Migrations
{
    /// <inheritdoc />
    public partial class APIGestionInventario8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 10, 31, 14, 38, 39, 0, DateTimeKind.Local).AddTicks(6590));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 10, 31, 14, 38, 39, 0, DateTimeKind.Local).AddTicks(6613));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "admin01",
                columns: new[] { "Estado", "FechaCreado" },
                values: new object[] { true, new DateTime(2024, 10, 31, 14, 38, 39, 0, DateTimeKind.Local).AddTicks(7027) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "empleado01",
                columns: new[] { "Estado", "FechaCreado" },
                values: new object[] { true, new DateTime(2024, 10, 31, 14, 38, 39, 0, DateTimeKind.Local).AddTicks(7036) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "Estado", "FechaCreado" },
                values: new object[] { false, new DateTime(2024, 10, 30, 13, 2, 9, 604, DateTimeKind.Local).AddTicks(4885) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "empleado01",
                columns: new[] { "Estado", "FechaCreado" },
                values: new object[] { false, new DateTime(2024, 10, 30, 13, 2, 9, 604, DateTimeKind.Local).AddTicks(4889) });
        }
    }
}
