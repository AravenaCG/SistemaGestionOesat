using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestionOrquesta.Migrations
{
    public partial class AgregarCamposTutorEstudiante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentoTutor",
                table: "Estudiante",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentoTutor2",
                table: "Estudiante",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreTutor2",
                table: "Estudiante",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelefonoTutor2",
                table: "Estudiante",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentoTutor",
                table: "Estudiante");

            migrationBuilder.DropColumn(
                name: "DocumentoTutor2",
                table: "Estudiante");

            migrationBuilder.DropColumn(
                name: "NombreTutor2",
                table: "Estudiante");

            migrationBuilder.DropColumn(
                name: "TelefonoTutor2",
                table: "Estudiante");
        }
    }
}
