using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIGestionInventario.Migrations
{
    /// <inheritdoc />
    public partial class GestionInventario5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 10, 30, 1, 37, 36, 82, DateTimeKind.Local).AddTicks(360));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 10, 30, 1, 37, 36, 82, DateTimeKind.Local).AddTicks(377));

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "ActualizadoPor", "CreadoPor", "Estado", "FechaActulizado", "FechaCreado", "Password", "RolId", "UsuarioApellido", "UsuarioNombre" },
                values: new object[] { "admin01", null, "admin01", false, null, new DateTime(2024, 10, 30, 1, 37, 36, 82, DateTimeKind.Local).AddTicks(529), "@1234Abc", 1, "System", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "admin01");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Usuarios");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 10, 30, 1, 33, 50, 727, DateTimeKind.Local).AddTicks(9036));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 10, 30, 1, 33, 50, 727, DateTimeKind.Local).AddTicks(9051));
        }
    }
}
