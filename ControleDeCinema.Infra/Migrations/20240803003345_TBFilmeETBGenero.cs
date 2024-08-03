using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeCinema.Infra.Migrations
{
    /// <inheritdoc />
    public partial class TBFilmeETBGenero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBGenero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBGenero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBFilme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(200)", nullable: false),
                    Genero_Id = table.Column<int>(type: "int", nullable: false),
                    Duracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estreia = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBFilme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBFilme_TBGenero_Genero_Id",
                        column: x => x.Genero_Id,
                        principalTable: "TBGenero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBFilme_Genero_Id",
                table: "TBFilme",
                column: "Genero_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBFilme");

            migrationBuilder.DropTable(
                name: "TBGenero");
        }
    }
}
