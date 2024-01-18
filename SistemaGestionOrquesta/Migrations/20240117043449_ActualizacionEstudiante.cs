using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestionOrquesta.Migrations
{
    public partial class ActualizacionEstudiante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instrumento",
                columns: table => new
                {
                    InstrumentoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Detalles = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrumento", x => x.InstrumentoID);
                });

            migrationBuilder.CreateTable(
                name: "Profesor",
                columns: table => new
                {
                    ProfesorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: true),
                    Documento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesor", x => x.ProfesorID);
                });

            migrationBuilder.CreateTable(
                name: "Estudiante",
                columns: table => new
                {
                    EstudianteID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: true),
                    Documento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InstrumentoID = table.Column<int>(type: "int", nullable: true),
                    RutaFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: true),
                    Nombre_Tutor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefono_Tutor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Asegurado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiante", x => x.EstudianteID);
                    table.ForeignKey(
                        name: "FK__Alumnos__Instrum__5629CD9C",
                        column: x => x.InstrumentoID,
                        principalTable: "Instrumento",
                        principalColumn: "InstrumentoID");
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    CursoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProfesorID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.CursoID);
                    table.ForeignKey(
                        name: "FK__Curso__ProfesorI__4CA06362",
                        column: x => x.ProfesorID,
                        principalTable: "Profesor",
                        principalColumn: "ProfesorID");
                });

            migrationBuilder.CreateTable(
                name: "PrestamoInstrumento",
                columns: table => new
                {
                    PrestamoInstrumentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaPrestamo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaDevolucion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InstrumentoId = table.Column<int>(type: "int", nullable: false),
                    EstudianteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestamoInstrumento", x => x.PrestamoInstrumentoId);
                    table.ForeignKey(
                        name: "FK_PrestamoInstrumento_Estudiante_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiante",
                        principalColumn: "EstudianteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrestamoInstrumento_Instrumento_InstrumentoId",
                        column: x => x.InstrumentoId,
                        principalTable: "Instrumento",
                        principalColumn: "InstrumentoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlumnosCurso",
                columns: table => new
                {
                    EstudianteID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CursoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AlumnosC__F7468990458DE6AB", x => new { x.EstudianteID, x.CursoID });
                    table.ForeignKey(
                        name: "FK__AlumnosCu__Alumn__5441852A",
                        column: x => x.EstudianteID,
                        principalTable: "Estudiante",
                        principalColumn: "EstudianteID");
                    table.ForeignKey(
                        name: "FK__AlumnosCu__Curso__5535A963",
                        column: x => x.CursoID,
                        principalTable: "Curso",
                        principalColumn: "CursoID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlumnosCurso_CursoID",
                table: "AlumnosCurso",
                column: "CursoID");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_ProfesorID",
                table: "Curso",
                column: "ProfesorID");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_InstrumentoID",
                table: "Estudiante",
                column: "InstrumentoID");

            migrationBuilder.CreateIndex(
                name: "IX_PrestamoInstrumento_EstudianteId",
                table: "PrestamoInstrumento",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_PrestamoInstrumento_InstrumentoId",
                table: "PrestamoInstrumento",
                column: "InstrumentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlumnosCurso");

            migrationBuilder.DropTable(
                name: "PrestamoInstrumento");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Estudiante");

            migrationBuilder.DropTable(
                name: "Profesor");

            migrationBuilder.DropTable(
                name: "Instrumento");
        }
    }
}
