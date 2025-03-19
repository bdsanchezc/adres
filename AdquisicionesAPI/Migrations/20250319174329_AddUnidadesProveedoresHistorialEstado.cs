using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdquisicionesAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUnidadesProveedoresHistorialEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Adquisiciones");

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Adquisiciones",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
                name: "HistorialAdquisiciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdquisicionId = table.Column<int>(type: "INTEGER", nullable: false),
                    DatosAnteriores = table.Column<string>(type: "TEXT", nullable: false),
                    FechaCambio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialAdquisiciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialAdquisiciones_Adquisiciones_AdquisicionId",
                        column: x => x.AdquisicionId,
                        principalTable: "Adquisiciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAdquisiciones_AdquisicionId",
                table: "HistorialAdquisiciones",
                column: "AdquisicionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "HistorialAdquisiciones");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Adquisiciones");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Adquisiciones",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
