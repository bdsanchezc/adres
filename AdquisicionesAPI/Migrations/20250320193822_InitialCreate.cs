using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdquisicionesAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistorialAdquisiciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdquisicionId = table.Column<int>(type: "INTEGER", nullable: false),
                    CampoModificado = table.Column<string>(type: "TEXT", nullable: false),
                    ValorAnterior = table.Column<string>(type: "TEXT", nullable: false),
                    ValorNuevo = table.Column<string>(type: "TEXT", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Usuario = table.Column<string>(type: "TEXT", nullable: false),
                    AccionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialAdquisiciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialAdquisiciones_Accion_AccionId",
                        column: x => x.AccionId,
                        principalTable: "Accion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adquisiciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Presupuesto = table.Column<decimal>(type: "TEXT", nullable: false),
                    UnidadId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoBienServicio = table.Column<string>(type: "TEXT", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "TEXT", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    FechaAdquisicion = table.Column<string>(type: "TEXT", nullable: false),
                    ProveedorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Documentacion = table.Column<string>(type: "TEXT", nullable: true),
                    EstadoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adquisiciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adquisiciones_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adquisiciones_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adquisiciones_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Accion",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Creación" },
                    { 2, "Edición" }
                });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Creado" },
                    { 2, "En proceso" },
                    { 3, "Inactivo" },
                    { 4, "Finalizado" }
                });

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Laboratorios Bayer S.A." },
                    { 2, "Laboratorios Distritales S.A." },
                    { 3, "Laboratirio Medicinal S.A." }
                });

            migrationBuilder.InsertData(
                table: "Unidades",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Dirección de Medicamentos y Tecnologías en Salud" },
                    { 2, "Dirección de Enfermería" },
                    { 3, "Dirección de Medicamentos básicos" },
                    { 4, "Dirección General de Salud" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adquisiciones_EstadoId",
                table: "Adquisiciones",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Adquisiciones_ProveedorId",
                table: "Adquisiciones",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Adquisiciones_UnidadId",
                table: "Adquisiciones",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAdquisiciones_AccionId",
                table: "HistorialAdquisiciones",
                column: "AccionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adquisiciones");

            migrationBuilder.DropTable(
                name: "HistorialAdquisiciones");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Unidades");

            migrationBuilder.DropTable(
                name: "Accion");
        }
    }
}
