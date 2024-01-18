using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestionOrquesta.Migrations
{
    public partial class ActualizacionPrestamo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrestamoInstrumento_Estudiante_EstudianteId",
                table: "PrestamoInstrumento");

            migrationBuilder.DropForeignKey(
                name: "FK_PrestamoInstrumento_Instrumento_InstrumentoId",
                table: "PrestamoInstrumento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrestamoInstrumento",
                table: "PrestamoInstrumento");

            migrationBuilder.RenameTable(
                name: "PrestamoInstrumento",
                newName: "PrestamosInstrumentos");

            migrationBuilder.RenameIndex(
                name: "IX_PrestamoInstrumento_InstrumentoId",
                table: "PrestamosInstrumentos",
                newName: "IX_PrestamosInstrumentos_InstrumentoId");

            migrationBuilder.RenameIndex(
                name: "IX_PrestamoInstrumento_EstudianteId",
                table: "PrestamosInstrumentos",
                newName: "IX_PrestamosInstrumentos_EstudianteId");

            migrationBuilder.AddColumn<bool>(
                name: "Disponible",
                table: "Instrumento",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrestamosInstrumentos",
                table: "PrestamosInstrumentos",
                column: "PrestamoInstrumentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrestamosInstrumentos_Estudiante_EstudianteId",
                table: "PrestamosInstrumentos",
                column: "EstudianteId",
                principalTable: "Estudiante",
                principalColumn: "EstudianteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrestamosInstrumentos_Instrumento_InstrumentoId",
                table: "PrestamosInstrumentos",
                column: "InstrumentoId",
                principalTable: "Instrumento",
                principalColumn: "InstrumentoID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrestamosInstrumentos_Estudiante_EstudianteId",
                table: "PrestamosInstrumentos");

            migrationBuilder.DropForeignKey(
                name: "FK_PrestamosInstrumentos_Instrumento_InstrumentoId",
                table: "PrestamosInstrumentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrestamosInstrumentos",
                table: "PrestamosInstrumentos");

            migrationBuilder.DropColumn(
                name: "Disponible",
                table: "Instrumento");

            migrationBuilder.RenameTable(
                name: "PrestamosInstrumentos",
                newName: "PrestamoInstrumento");

            migrationBuilder.RenameIndex(
                name: "IX_PrestamosInstrumentos_InstrumentoId",
                table: "PrestamoInstrumento",
                newName: "IX_PrestamoInstrumento_InstrumentoId");

            migrationBuilder.RenameIndex(
                name: "IX_PrestamosInstrumentos_EstudianteId",
                table: "PrestamoInstrumento",
                newName: "IX_PrestamoInstrumento_EstudianteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrestamoInstrumento",
                table: "PrestamoInstrumento",
                column: "PrestamoInstrumentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrestamoInstrumento_Estudiante_EstudianteId",
                table: "PrestamoInstrumento",
                column: "EstudianteId",
                principalTable: "Estudiante",
                principalColumn: "EstudianteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrestamoInstrumento_Instrumento_InstrumentoId",
                table: "PrestamoInstrumento",
                column: "InstrumentoId",
                principalTable: "Instrumento",
                principalColumn: "InstrumentoID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
