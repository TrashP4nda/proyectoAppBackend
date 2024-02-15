using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoApi.Migrations
{
    /// <inheritdoc />
    public partial class customIncidencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomIncidencias_Incidencias_Id",
                table: "CustomIncidencias");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CustomIncidencias",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "AutonomousRegion",
                table: "CustomIncidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CarRegistration",
                table: "CustomIncidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Cause",
                table: "CustomIncidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CityTown",
                table: "CustomIncidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Direction",
                table: "CustomIncidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IncidenceLevel",
                table: "CustomIncidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "endDate",
                table: "CustomIncidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "incidenceDescription",
                table: "CustomIncidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "incidenceId",
                table: "CustomIncidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "latitude",
                table: "CustomIncidencias",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "longitude",
                table: "CustomIncidencias",
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
                name: "AutonomousRegion",
                table: "CustomIncidencias");

            migrationBuilder.DropColumn(
                name: "CarRegistration",
                table: "CustomIncidencias");

            migrationBuilder.DropColumn(
                name: "Cause",
                table: "CustomIncidencias");

            migrationBuilder.DropColumn(
                name: "CityTown",
                table: "CustomIncidencias");

            migrationBuilder.DropColumn(
                name: "Direction",
                table: "CustomIncidencias");

            migrationBuilder.DropColumn(
                name: "IncidenceLevel",
                table: "CustomIncidencias");

            migrationBuilder.DropColumn(
                name: "endDate",
                table: "CustomIncidencias");

            migrationBuilder.DropColumn(
                name: "incidenceDescription",
                table: "CustomIncidencias");

            migrationBuilder.DropColumn(
                name: "incidenceId",
                table: "CustomIncidencias");

            migrationBuilder.DropColumn(
                name: "latitude",
                table: "CustomIncidencias");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "CustomIncidencias");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CustomIncidencias",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomIncidencias_Incidencias_Id",
                table: "CustomIncidencias",
                column: "Id",
                principalTable: "Incidencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
