using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewRepository.Migrations
{
    /// <inheritdoc />
    public partial class updateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Livros_Isbn",
                table: "Livros",
                column: "Isbn");

            migrationBuilder.CreateTable(
                name: "InstituicaoLivros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Isbn = table.Column<string>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstituicaoLivros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstituicaoLivros_Instituicoes_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Instituicoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstituicaoLivros_Livros_Isbn",
                        column: x => x.Isbn,
                        principalTable: "Livros",
                        principalColumn: "Isbn",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstituicaoLivros_Isbn",
                table: "InstituicaoLivros",
                column: "Isbn");

            migrationBuilder.CreateIndex(
                name: "IX_InstituicaoLivros_UsuarioId",
                table: "InstituicaoLivros",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstituicaoLivros");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Livros_Isbn",
                table: "Livros");
        }
    }
}
