using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewRepository.Migrations
{
    /// <inheritdoc />
    public partial class atualizarDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstituicaoLivros_Livros_Isbn",
                table: "InstituicaoLivros");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Livros_Isbn",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_InstituicaoLivros_Isbn",
                table: "InstituicaoLivros");

            migrationBuilder.DropIndex(
                name: "IX_InstituicaoLivros_UsuarioId",
                table: "InstituicaoLivros");

            migrationBuilder.DropColumn(
                name: "Isbn",
                table: "InstituicaoLivros");

            migrationBuilder.AddColumn<int>(
                name: "LivroId",
                table: "InstituicaoLivros",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Livros_Isbn",
                table: "Livros",
                column: "Isbn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstituicaoLivros_LivroId",
                table: "InstituicaoLivros",
                column: "LivroId");

            migrationBuilder.CreateIndex(
                name: "IX_InstituicaoLivros_UsuarioId_LivroId",
                table: "InstituicaoLivros",
                columns: new[] { "UsuarioId", "LivroId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InstituicaoLivros_Livros_LivroId",
                table: "InstituicaoLivros",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstituicaoLivros_Livros_LivroId",
                table: "InstituicaoLivros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_Isbn",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_InstituicaoLivros_LivroId",
                table: "InstituicaoLivros");

            migrationBuilder.DropIndex(
                name: "IX_InstituicaoLivros_UsuarioId_LivroId",
                table: "InstituicaoLivros");

            migrationBuilder.DropColumn(
                name: "LivroId",
                table: "InstituicaoLivros");

            migrationBuilder.AddColumn<string>(
                name: "Isbn",
                table: "InstituicaoLivros",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Livros_Isbn",
                table: "Livros",
                column: "Isbn");

            migrationBuilder.CreateIndex(
                name: "IX_InstituicaoLivros_Isbn",
                table: "InstituicaoLivros",
                column: "Isbn");

            migrationBuilder.CreateIndex(
                name: "IX_InstituicaoLivros_UsuarioId",
                table: "InstituicaoLivros",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstituicaoLivros_Livros_Isbn",
                table: "InstituicaoLivros",
                column: "Isbn",
                principalTable: "Livros",
                principalColumn: "Isbn",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
