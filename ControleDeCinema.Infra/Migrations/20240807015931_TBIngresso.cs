using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeCinema.Infra.Migrations
{
    /// <inheritdoc />
    public partial class TBIngresso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBIngresso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroAssento = table.Column<int>(type: "int", nullable: false),
                    MeiaEntrada = table.Column<bool>(type: "bit", nullable: false),
                    SessaoId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_TBIngresso_TBSessao_SessaoId",
                        column: x => x.SessaoId,
                        principalTable: "TBSessao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBIngresso_FuncionarioId",
                table: "TBIngresso",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TBIngresso_SessaoId",
                table: "TBIngresso",
                column: "SessaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBIngresso");
        }
    }
}
