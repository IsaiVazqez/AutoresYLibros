using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAutoresyLibros.Migrations
{
    /// <inheritdoc />
    public partial class AutoresLibros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "autoresYLibros",
                columns: table => new
                {
                    LibroId = table.Column<int>(type: "int", nullable: false),
                    AutorId = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    LibrosId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_autoresYLibros", x => new { x.AutorId, x.LibroId });
                    table.ForeignKey(
                        name: "FK_autoresYLibros_Autores_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Autores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_autoresYLibros_Libros_LibrosId",
                        column: x => x.LibroId,
                        principalTable: "Libros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_autoresYLibros_LibrosId",
                table: "autoresYLibros",
                column: "LibroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "autoresYLibros");
        }
    }
}
