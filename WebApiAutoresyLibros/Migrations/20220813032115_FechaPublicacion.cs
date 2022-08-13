using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAutoresyLibros.Migrations
{
    /// <inheritdoc />
    public partial class FechaPublicacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_autoresYLibros_Libros_LibrosId",
                table: "autoresYLibros");

            migrationBuilder.DropIndex(
                name: "IX_autoresYLibros_LibrosId",
                table: "autoresYLibros");

            migrationBuilder.DropColumn(
                name: "LibrosId",
                table: "autoresYLibros");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPublicacion",
                table: "Libros",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_autoresYLibros_LibroId",
                table: "autoresYLibros",
                column: "LibroId");

            migrationBuilder.AddForeignKey(
                name: "FK_autoresYLibros_Libros_LibroId",
                table: "autoresYLibros",
                column: "LibroId",
                principalTable: "Libros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_autoresYLibros_Libros_LibroId",
                table: "autoresYLibros");

            migrationBuilder.DropIndex(
                name: "IX_autoresYLibros_LibroId",
                table: "autoresYLibros");

            migrationBuilder.DropColumn(
                name: "FechaPublicacion",
                table: "Libros");

            migrationBuilder.AddColumn<int>(
                name: "LibrosId",
                table: "autoresYLibros",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_autoresYLibros_LibrosId",
                table: "autoresYLibros",
                column: "LibrosId");

            migrationBuilder.AddForeignKey(
                name: "FK_autoresYLibros_Libros_LibrosId",
                table: "autoresYLibros",
                column: "LibrosId",
                principalTable: "Libros",
                principalColumn: "Id");
        }
    }
}
