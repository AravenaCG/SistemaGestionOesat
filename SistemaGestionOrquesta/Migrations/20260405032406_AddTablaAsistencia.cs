using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestionOrquesta.Migrations
{
    public partial class AddTablaAsistencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asistencia",
                columns: table => new
                {
                    AsistenciaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudianteID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CursoID = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Presente = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencia", x => x.AsistenciaID);
                    table.ForeignKey(
                        name: "FK_Asistencia_Curso_CursoID",
                        column: x => x.CursoID,
                        principalTable: "Curso",
                        principalColumn: "CursoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asistencia_Estudiante_EstudianteID",
                        column: x => x.EstudianteID,
                        principalTable: "Estudiante",
                        principalColumn: "EstudianteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_CursoID",
                table: "Asistencia",
                column: "CursoID");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_Estudiante_Curso_Fecha",
                table: "Asistencia",
                columns: new[] { "EstudianteID", "CursoID", "Fecha" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencia");
        }
    }
}
