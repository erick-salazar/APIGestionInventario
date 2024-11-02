using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIGestionInventario.Migrations
{
    /// <inheritdoc />
    public partial class APIGestionInventario14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesReposiciones_OrdenReposionEstado_OrdenReposiconEstadoId",
                table: "OrdenesReposiciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdenReposionEstado",
                table: "OrdenReposionEstado");

            migrationBuilder.RenameTable(
                name: "OrdenReposionEstado",
                newName: "OrdenReposionEstados");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdenReposionEstados",
                table: "OrdenReposionEstados",
                column: "OrdenReposicionEstadoId");

            migrationBuilder.UpdateData(
                table: "OrdenReposionEstados",
                keyColumn: "OrdenReposicionEstadoId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 20, 11, 40, 148, DateTimeKind.Local).AddTicks(6698));

            migrationBuilder.UpdateData(
                table: "OrdenReposionEstados",
                keyColumn: "OrdenReposicionEstadoId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 20, 11, 40, 148, DateTimeKind.Local).AddTicks(6702));

            migrationBuilder.UpdateData(
                table: "OrdenReposionEstados",
                keyColumn: "OrdenReposicionEstadoId",
                keyValue: 3,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 20, 11, 40, 148, DateTimeKind.Local).AddTicks(6700));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 20, 11, 40, 148, DateTimeKind.Local).AddTicks(6660));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 20, 11, 40, 148, DateTimeKind.Local).AddTicks(6668));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "ProveedorId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 20, 11, 40, 148, DateTimeKind.Local).AddTicks(6625));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "ProveedorId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 20, 11, 40, 148, DateTimeKind.Local).AddTicks(6631));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 20, 11, 40, 148, DateTimeKind.Local).AddTicks(6303));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 20, 11, 40, 148, DateTimeKind.Local).AddTicks(6320));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "admin01",
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 20, 11, 40, 148, DateTimeKind.Local).AddTicks(6582));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "empleado01",
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 20, 11, 40, 148, DateTimeKind.Local).AddTicks(6587));

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesReposiciones_OrdenReposionEstados_OrdenReposiconEstadoId",
                table: "OrdenesReposiciones",
                column: "OrdenReposiconEstadoId",
                principalTable: "OrdenReposionEstados",
                principalColumn: "OrdenReposicionEstadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesReposiciones_OrdenReposionEstados_OrdenReposiconEstadoId",
                table: "OrdenesReposiciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdenReposionEstados",
                table: "OrdenReposionEstados");

            migrationBuilder.RenameTable(
                name: "OrdenReposionEstados",
                newName: "OrdenReposionEstado");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdenReposionEstado",
                table: "OrdenReposionEstado",
                column: "OrdenReposicionEstadoId");

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
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7620));

            migrationBuilder.UpdateData(
                table: "OrdenReposionEstado",
                keyColumn: "OrdenReposicionEstadoId",
                keyValue: 3,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 16, 28, 18, 127, DateTimeKind.Local).AddTicks(7618));

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

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesReposiciones_OrdenReposionEstado_OrdenReposiconEstadoId",
                table: "OrdenesReposiciones",
                column: "OrdenReposiconEstadoId",
                principalTable: "OrdenReposionEstado",
                principalColumn: "OrdenReposicionEstadoId");
        }
    }
}
