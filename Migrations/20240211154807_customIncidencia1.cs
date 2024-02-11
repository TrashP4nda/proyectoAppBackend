using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoApi.Migrations
{
    /// <inheritdoc />
    public partial class customIncidencia1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Incidencias");

            migrationBuilder.CreateTable(
                name: "CustomIncidencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomIncidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomIncidencias_Incidencias_Id",
                        column: x => x.Id,
                        principalTable: "Incidencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomIncidencias");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Incidencias",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
