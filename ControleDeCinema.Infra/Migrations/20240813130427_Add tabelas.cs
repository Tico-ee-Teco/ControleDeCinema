using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ControleDeCinema.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Addtabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TBFuncionario",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TBFuncionario",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TBFuncionario",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TBGenero",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TBGenero",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TBGenero",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TBGenero",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TBIngresso",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TBIngresso",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TBIngresso",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TBSessao",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TBSessao",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TBSessao",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TBSessao",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TBFilme",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TBSala",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TBSessao",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TBSessao",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TBFilme",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TBGenero",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TBSala",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TBSala",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TBGenero",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "Usuario_Id",
                table: "TBSessao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Usuario_Id",
                table: "TBSala",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Usuario_Id",
                table: "TBIngresso",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Usuario_Id",
                table: "TBGenero",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Usuario_Id",
                table: "TBFuncionario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Usuario_Id",
                table: "TBFilme",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBSessao_Usuario_Id",
                table: "TBSessao",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBSala_Usuario_Id",
                table: "TBSala",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBIngresso_Usuario_Id",
                table: "TBIngresso",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBGenero_Usuario_Id",
                table: "TBGenero",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBFuncionario_Usuario_Id",
                table: "TBFuncionario",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBFilme_Usuario_Id",
                table: "TBFilme",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TBFilme_AspNetUsers_Usuario_Id",
                table: "TBFilme",
                column: "Usuario_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBFuncionario_AspNetUsers_Usuario_Id",
                table: "TBFuncionario",
                column: "Usuario_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBGenero_AspNetUsers_Usuario_Id",
                table: "TBGenero",
                column: "Usuario_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBIngresso_AspNetUsers_Usuario_Id",
                table: "TBIngresso",
                column: "Usuario_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBSala_AspNetUsers_Usuario_Id",
                table: "TBSala",
                column: "Usuario_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBSessao_AspNetUsers_Usuario_Id",
                table: "TBSessao",
                column: "Usuario_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBFilme_AspNetUsers_Usuario_Id",
                table: "TBFilme");

            migrationBuilder.DropForeignKey(
                name: "FK_TBFuncionario_AspNetUsers_Usuario_Id",
                table: "TBFuncionario");

            migrationBuilder.DropForeignKey(
                name: "FK_TBGenero_AspNetUsers_Usuario_Id",
                table: "TBGenero");

            migrationBuilder.DropForeignKey(
                name: "FK_TBIngresso_AspNetUsers_Usuario_Id",
                table: "TBIngresso");

            migrationBuilder.DropForeignKey(
                name: "FK_TBSala_AspNetUsers_Usuario_Id",
                table: "TBSala");

            migrationBuilder.DropForeignKey(
                name: "FK_TBSessao_AspNetUsers_Usuario_Id",
                table: "TBSessao");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_TBSessao_Usuario_Id",
                table: "TBSessao");

            migrationBuilder.DropIndex(
                name: "IX_TBSala_Usuario_Id",
                table: "TBSala");

            migrationBuilder.DropIndex(
                name: "IX_TBIngresso_Usuario_Id",
                table: "TBIngresso");

            migrationBuilder.DropIndex(
                name: "IX_TBGenero_Usuario_Id",
                table: "TBGenero");

            migrationBuilder.DropIndex(
                name: "IX_TBFuncionario_Usuario_Id",
                table: "TBFuncionario");

            migrationBuilder.DropIndex(
                name: "IX_TBFilme_Usuario_Id",
                table: "TBFilme");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "TBSessao");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "TBSala");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "TBIngresso");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "TBGenero");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "TBFuncionario");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "TBFilme");

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
        }
    }
}
