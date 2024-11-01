using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIGestionInventario.Migrations
{
    /// <inheritdoc />
    public partial class APIGestionInventario12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "OrdenesReposiciones");

            migrationBuilder.AddColumn<int>(
                name: "OrdenReposiconEstadoId",
                table: "OrdenesReposiciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrdenReposionEstado",
                columns: table => new
                {
                    OrdenReposicionEstadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActulizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenReposionEstado", x => x.OrdenReposicionEstadoId);
                });

            migrationBuilder.InsertData(
                table: "OrdenReposionEstado",
                columns: new[] { "OrdenReposicionEstadoId", "ActualizadoPor", "CreadoPor", "Estado", "EstadoNombre", "FechaActulizado", "FechaCreado" },
                values: new object[,]
                {
                    { 1, null, "admin01", true, "Pendiente", null, new DateTime(2024, 11, 1, 16, 26, 56, 702, DateTimeKind.Local).AddTicks(8255) },
                    { 2, null, "admin01", true, "Cancelado", null, new DateTime(2024, 11, 1, 16, 26, 56, 702, DateTimeKind.Local).AddTicks(8259) },
                    { 3, null, "admin01", true, "Realizado", null, new DateTime(2024, 11, 1, 16, 26, 56, 702, DateTimeKind.Local).AddTicks(8262) }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesReposiciones_OrdenReposiconEstadoId",
                table: "OrdenesReposiciones",
                column: "OrdenReposiconEstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesReposiciones_OrdenReposionEstado_OrdenReposiconEstadoId",
                table: "OrdenesReposiciones",
                column: "OrdenReposiconEstadoId",
                principalTable: "OrdenReposionEstado",
                principalColumn: "OrdenReposicionEstadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesReposiciones_OrdenReposionEstado_OrdenReposiconEstadoId",
                table: "OrdenesReposiciones");

            migrationBuilder.DropTable(
                name: "OrdenReposionEstado");

            migrationBuilder.DropIndex(
                name: "IX_OrdenesReposiciones_OrdenReposiconEstadoId",
                table: "OrdenesReposiciones");

            migrationBuilder.DropColumn(
                name: "OrdenReposiconEstadoId",
                table: "OrdenesReposiciones");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "OrdenesReposiciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
        }
    }
}
