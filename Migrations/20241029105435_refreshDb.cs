using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewRepository.Migrations
{
    /// <inheritdoc />
    public partial class refreshDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAdd",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "QtdLivro",
                table: "Livros");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAdd",
                table: "Livros",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "QtdLivro",
                table: "Livros",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
