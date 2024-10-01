using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewRepository.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoInstituicoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Bibliotecas");

            migrationBuilder.CreateTable(
                name: "Instituicoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    RazaoSocial = table.Column<string>(type: "TEXT", nullable: false),
                    NomeFantasia = table.Column<string>(type: "TEXT", nullable: false),
                    Cnpj = table.Column<string>(type: "TEXT", nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", nullable: false),
                    Endereco = table.Column<string>(type: "TEXT", nullable: false),
                    SenhaHash = table.Column<byte[]>(type: "BLOB", nullable: false),
                    SenhaSalt = table.Column<byte[]>(type: "BLOB", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituicoes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instituicoes");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Bibliotecas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
