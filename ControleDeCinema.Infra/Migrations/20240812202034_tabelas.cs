using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ControleDeCinema.Infra.Migrations
{
    /// <inheritdoc />
    public partial class tabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBFuncionario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    CPF = table.Column<string>(type: "varchar(11)", nullable: false),
                    Login = table.Column<string>(type: "varchar(200)", nullable: false),
                    Senha = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBFuncionario", x => x.Id);
                });

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
                name: "TBSala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Capacidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSala", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBFilme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(200)", nullable: false),
                    Genero_Id = table.Column<int>(type: "int", nullable: false),
                    Duracao = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "TBSessao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroMaximoIngressos = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime", nullable: false),
                    Sala_Id = table.Column<int>(type: "int", nullable: false),
                    Filme_Id = table.Column<int>(type: "int", nullable: false),
                    Encerrada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSessao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBSessao_TBFilme_Filme_Id",
                        column: x => x.Filme_Id,
                        principalTable: "TBFilme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBSessao_TBSala_Sala_Id",
                        column: x => x.Sala_Id,
                        principalTable: "TBSala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBIngresso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroAssento = table.Column<int>(type: "int", nullable: false),
                    MeiaEntrada = table.Column<bool>(type: "bit", nullable: false),
                    Sessao_Id = table.Column<int>(type: "int", nullable: false),
                    FuncionarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBIngresso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBIngresso_TBFuncionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "TBFuncionario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TBIngresso_TBSessao_Sessao_Id",
                        column: x => x.Sessao_Id,
                        principalTable: "TBSessao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TBFuncionario",
                columns: new[] { "Id", "CPF", "Login", "Nome", "Senha" },
                values: new object[,]
                {
                    { 1, "12345678900", "c.tanaka", "Caio Tanaka", "sFQZT5W2kK8BUAO8uhhQ" },
                    { 2, "98765432100", "junior.teixeira201", "Júnior Teixeira", "eNsoNQxmzglCOs3OK76a" },
                    { 3, "45678912300", "marcia.silva0306", "Márcia Silva", "AW6m9OHzgB28v4ZNS5jY" }
                });

            migrationBuilder.InsertData(
                table: "TBGenero",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Ação" },
                    { 2, "Animação" },
                    { 3, "Aventura" },
                    { 4, "Comédia" },
                    { 5, "Romance" },
                    { 6, "Terror" }
                });

            migrationBuilder.InsertData(
                table: "TBSala",
                columns: new[] { "Id", "Capacidade", "Numero" },
                values: new object[,]
                {
                    { 1, 30, 1 },
                    { 2, 35, 2 },
                    { 3, 32, 5 }
                });

            migrationBuilder.InsertData(
                table: "TBFilme",
                columns: new[] { "Id", "Duracao", "Estreia", "Genero_Id", "Titulo" },
                values: new object[,]
                {
                    { 1, 90, false, 2, "Aladdin" },
                    { 2, 127, true, 1, "Wolverine vs. Deadpool" }
                });

            migrationBuilder.InsertData(
                table: "TBSessao",
                columns: new[] { "Id", "Data", "Encerrada", "Filme_Id", "NumeroMaximoIngressos", "Sala_Id" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 20, 20, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 20, 2 },
                    { 2, new DateTime(2024, 8, 9, 17, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 25, 3 },
                    { 3, new DateTime(2024, 8, 10, 20, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 35, 3 },
                    { 4, new DateTime(2024, 8, 7, 20, 0, 0, 0, DateTimeKind.Unspecified), false, 2, 25, 2 },
                    { 5, new DateTime(2024, 8, 2, 19, 30, 0, 0, DateTimeKind.Unspecified), true, 2, 28, 2 },
                    { 6, new DateTime(2024, 8, 2, 19, 30, 0, 0, DateTimeKind.Unspecified), false, 2, 30, 1 }
                });

            migrationBuilder.InsertData(
                table: "TBIngresso",
                columns: new[] { "Id", "FuncionarioId", "MeiaEntrada", "NumeroAssento", "Sessao_Id" },
                values: new object[,]
                {
                    { 1, null, false, 10, 1 },
                    { 2, null, true, 25, 1 },
                    { 3, null, false, 30, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBFilme_Genero_Id",
                table: "TBFilme",
                column: "Genero_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBIngresso_FuncionarioId",
                table: "TBIngresso",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TBIngresso_Sessao_Id",
                table: "TBIngresso",
                column: "Sessao_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBSessao_Filme_Id",
                table: "TBSessao",
                column: "Filme_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBSessao_Sala_Id",
                table: "TBSessao",
                column: "Sala_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBIngresso");

            migrationBuilder.DropTable(
                name: "TBFuncionario");

            migrationBuilder.DropTable(
                name: "TBSessao");

            migrationBuilder.DropTable(
                name: "TBFilme");

            migrationBuilder.DropTable(
                name: "TBSala");

            migrationBuilder.DropTable(
                name: "TBGenero");
        }
    }
}
