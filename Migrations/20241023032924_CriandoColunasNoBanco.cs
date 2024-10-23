using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewRepository.Migrations
{
    /// <inheritdoc />
    public partial class CriandoColunasNoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FonteCadastro",
                table: "Livros",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "QtdLivro",
                table: "Livros",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FonteCadastro",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "QtdLivro",
                table: "Livros");
        }
    }
}
