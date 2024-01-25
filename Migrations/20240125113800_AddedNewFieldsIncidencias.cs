using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewFieldsIncidencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CityTown",
                table: "Incidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Direction",
                table: "Incidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IncidenceLevel",
                table: "Incidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "endDate",
                table: "Incidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "incidenceDescription",
                table: "Incidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "incidenceID",
                table: "Incidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "CityTown",
                table: "Incidencias");

            migrationBuilder.DropColumn(
                name: "Direction",
                table: "Incidencias");

            migrationBuilder.DropColumn(
                name: "IncidenceLevel",
                table: "Incidencias");

            migrationBuilder.DropColumn(
                name: "endDate",
                table: "Incidencias");

            migrationBuilder.DropColumn(
                name: "incidenceDescription",
                table: "Incidencias");

            migrationBuilder.DropColumn(
                name: "incidenceID",
                table: "Incidencias");
        }
    }
}
