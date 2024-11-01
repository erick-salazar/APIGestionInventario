using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIGestionInventario.Migrations
{
    /// <inheritdoc />
    public partial class APIGestionInventario13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRealizado",
                table: "OrdenesReposiciones",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "OrdenReposionEstado",
                keyColumn: "OrdenReposicionEstadoId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7613));

            migrationBuilder.UpdateData(
                table: "OrdenReposionEstado",
                keyColumn: "OrdenReposicionEstadoId",
                keyValue: 2,
                columns: new[] { "EstadoNombre", "FechaCreado" },
                values: new object[] { "Realizado", new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "OrdenReposionEstado",
                keyColumn: "OrdenReposicionEstadoId",
                keyValue: 3,
                columns: new[] { "EstadoNombre", "FechaCreado" },
                values: new object[] { "Cancelado", new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7618) });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7543));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7552));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "ProveedorId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7479));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "ProveedorId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7492));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7037));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7057));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "admin01",
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7424));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "empleado01",
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7429));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaRealizado",
                table: "OrdenesReposiciones");

            migrationBuilder.UpdateData(
                table: "OrdenReposionEstado",
                keyColumn: "OrdenReposicionEstadoId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 26, 56, 702, DateTimeKind.Local).AddTicks(8255));

            migrationBuilder.UpdateData(
                table: "OrdenReposionEstado",
                keyColumn: "OrdenReposicionEstadoId",
                keyValue: 2,
                columns: new[] { "EstadoNombre", "FechaCreado" },
                values: new object[] { "Cancelado", new DateTime(2024, 11, 1, 16, 26, 56, 702, DateTimeKind.Local).AddTicks(8259) });

            migrationBuilder.UpdateData(
                table: "OrdenReposionEstado",
                keyColumn: "OrdenReposicionEstadoId",
                keyValue: 3,
                columns: new[] { "EstadoNombre", "FechaCreado" },
                values: new object[] { "Realizado", new DateTime(2024, 11, 1, 16, 26, 56, 702, DateTimeKind.Local).AddTicks(8262) });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 26, 56, 702, DateTimeKind.Local).AddTicks(8176));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 26, 56, 702, DateTimeKind.Local).AddTicks(8185));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "ProveedorId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 26, 56, 702, DateTimeKind.Local).AddTicks(8107));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "ProveedorId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 26, 56, 702, DateTimeKind.Local).AddTicks(8117));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 26, 56, 702, DateTimeKind.Local).AddTicks(7551));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 26, 56, 702, DateTimeKind.Local).AddTicks(7571));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "admin01",
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 26, 56, 702, DateTimeKind.Local).AddTicks(8031));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "empleado01",
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 26, 56, 702, DateTimeKind.Local).AddTicks(8037));
        }
    }
}
