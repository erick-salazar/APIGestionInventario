using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIGestionInventario.Migrations
{
    /// <inheritdoc />
    public partial class APIGestionInventario10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProveedorId",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProveedorNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActulizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.ProveedorId);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesReposiciones",
                columns: table => new
                {
                    OrdenReposicionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false),
                    ProductoCantidad = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActulizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesReposiciones", x => x.OrdenReposicionId);
                    table.ForeignKey(
                        name: "FK_OrdenesReposiciones_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesReposiciones_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "ProveedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "ProveedorId", "ActualizadoPor", "CreadoPor", "Estado", "FechaActulizado", "FechaCreado", "ProveedorNombre" },
                values: new object[,]
                {
                    { 1, null, "admin01", true, null, new DateTime(2024, 11, 1, 12, 59, 20, 421, DateTimeKind.Local).AddTicks(7934), "Dell" },
                    { 2, null, "admin01", true, null, new DateTime(2024, 11, 1, 12, 59, 20, 421, DateTimeKind.Local).AddTicks(7940), "Lenovo" }
                });

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

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "ProductoId", "ActualizadoPor", "CreadoPor", "Estado", "FechaActulizado", "FechaCreado", "ProductoCantidad", "ProductoCantidadMinima", "ProductoDescripcion", "ProductoNombre", "ProductoPrecio", "ProveedorId" },
                values: new object[,]
                {
                    { 1, null, "admin01", true, null, new DateTime(2024, 11, 1, 12, 59, 20, 421, DateTimeKind.Local).AddTicks(7972), 10, 2, "I7 Dell", "Dell Latitude", 0m, 1 },
                    { 2, null, "admin01", true, null, new DateTime(2024, 11, 1, 12, 59, 20, 421, DateTimeKind.Local).AddTicks(7978), 10, 2, "I7 Lenovo", "Lenovo ThinkPad", 0m, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ProveedorId",
                table: "Productos",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesReposiciones_ProductoId",
                table: "OrdenesReposiciones",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesReposiciones_ProveedorId",
                table: "OrdenesReposiciones",
                column: "ProveedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Proveedores_ProveedorId",
                table: "Productos",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "ProveedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Proveedores_ProveedorId",
                table: "Productos");

            migrationBuilder.DropTable(
                name: "OrdenesReposiciones");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropIndex(
                name: "IX_Productos_ProveedorId",
                table: "Productos");

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "ProveedorId",
                table: "Productos");

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
                column: "FechaCreado",
                value: new DateTime(2024, 10, 31, 14, 38, 39, 0, DateTimeKind.Local).AddTicks(7027));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: "empleado01",
                column: "FechaCreado",
                value: new DateTime(2024, 10, 31, 14, 38, 39, 0, DateTimeKind.Local).AddTicks(7036));
        }
    }
}
