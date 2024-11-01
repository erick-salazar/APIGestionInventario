using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIGestionInventario.Migrations
{
    /// <inheritdoc />
    public partial class APIGestionInventario11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesReposiciones_Productos_ProductoId",
                table: "OrdenesReposiciones");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesReposiciones_Proveedores_ProveedorId",
                table: "OrdenesReposiciones");

            migrationBuilder.AddColumn<int>(
                name: "OrdenCompraId",
                table: "OrdenesReposiciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 15, 14, 27, 114, DateTimeKind.Local).AddTicks(4273));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 15, 14, 27, 114, DateTimeKind.Local).AddTicks(4281));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "ProveedorId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 15, 14, 27, 114, DateTimeKind.Local).AddTicks(4221));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "ProveedorId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 15, 14, 27, 114, DateTimeKind.Local).AddTicks(4232));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 15, 14, 27, 114, DateTimeKind.Local).AddTicks(3606));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 15, 14, 27, 114, DateTimeKind.Local).AddTicks(3629));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "admin01",
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 15, 14, 27, 114, DateTimeKind.Local).AddTicks(4163));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "empleado01",
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 15, 14, 27, 114, DateTimeKind.Local).AddTicks(4172));

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesReposiciones_OrdenCompraId",
                table: "OrdenesReposiciones",
                column: "OrdenCompraId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesReposiciones_OrdenesCompras_OrdenCompraId",
                table: "OrdenesReposiciones",
                column: "OrdenCompraId",
                principalTable: "OrdenesCompras",
                principalColumn: "OrdenCompraId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesReposiciones_Productos_ProductoId",
                table: "OrdenesReposiciones",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesReposiciones_Proveedores_ProveedorId",
                table: "OrdenesReposiciones",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "ProveedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesReposiciones_OrdenesCompras_OrdenCompraId",
                table: "OrdenesReposiciones");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesReposiciones_Productos_ProductoId",
                table: "OrdenesReposiciones");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesReposiciones_Proveedores_ProveedorId",
                table: "OrdenesReposiciones");

            migrationBuilder.DropIndex(
                name: "IX_OrdenesReposiciones_OrdenCompraId",
                table: "OrdenesReposiciones");

            migrationBuilder.DropColumn(
                name: "OrdenCompraId",
                table: "OrdenesReposiciones");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 12, 59, 20, 421, DateTimeKind.Local).AddTicks(7972));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 12, 59, 20, 421, DateTimeKind.Local).AddTicks(7978));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "ProveedorId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 12, 59, 20, 421, DateTimeKind.Local).AddTicks(7934));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "ProveedorId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 12, 59, 20, 421, DateTimeKind.Local).AddTicks(7940));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 1,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 12, 59, 20, 421, DateTimeKind.Local).AddTicks(7552));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RolId",
                keyValue: 2,
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 12, 59, 20, 421, DateTimeKind.Local).AddTicks(7570));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "admin01",
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 12, 59, 20, 421, DateTimeKind.Local).AddTicks(7897));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "empleado01",
                column: "FechaCreado",
                value: new DateTime(2024, 11, 1, 12, 59, 20, 421, DateTimeKind.Local).AddTicks(7901));

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesReposiciones_Productos_ProductoId",
                table: "OrdenesReposiciones",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesReposiciones_Proveedores_ProveedorId",
                table: "OrdenesReposiciones",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "ProveedorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
