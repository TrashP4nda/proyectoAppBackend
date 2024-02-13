using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoApi.Migrations
{
    /// <inheritdoc />
    public partial class ArregloCam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "incidenceID",
                table: "Incidencias",
                newName: "incidenceId");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "Camaras",
                newName: "urlImage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "incidenceId",
                table: "Incidencias",
                newName: "incidenceID");

            migrationBuilder.RenameColumn(
                name: "urlImage",
                table: "Camaras",
                newName: "url");
        }
    }
}
