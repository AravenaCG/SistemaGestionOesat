using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestionOrquesta.Migrations
{
    public partial class AgregarStockCorregido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Solo dejá esto:
            migrationBuilder.AddColumn<int>(name: "StockInstrumentoId", table: "PrestamosInstrumentos", type: "int", nullable: true);

            migrationBuilder.CreateTable(
                name: "StockInstrumento",
                columns: table => new {
                    StockInstrumentoID = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    CodigoInventario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroSerie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "('Disponible')"),
                    InstrumentoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_StockInstrumento", x => x.StockInstrumentoID);
                    table.ForeignKey(name: "FK_StockInstrumento_Instrumento", column: x => x.InstrumentoID, principalTable: "Instrumento", principalColumn: "InstrumentoID");
                });

            migrationBuilder.CreateIndex(name: "IX_PrestamosInstrumentos_StockInstrumentoId", table: "PrestamosInstrumentos", column: "StockInstrumentoId");
            migrationBuilder.CreateIndex(name: "IX_StockInstrumento_InstrumentoID", table: "StockInstrumento", column: "InstrumentoID");
            migrationBuilder.AddForeignKey(name: "FK_Prestamos_StockInstrumento", table: "PrestamosInstrumentos", column: "StockInstrumentoId", principalTable: "StockInstrumento", principalColumn: "StockInstrumentoID");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Prestamos_StockInstrumento", table: "PrestamosInstrumentos");
            migrationBuilder.DropTable(name: "StockInstrumento");
            migrationBuilder.DropIndex(name: "IX_PrestamosInstrumentos_StockInstrumentoId", table: "PrestamosInstrumentos");
            migrationBuilder.DropColumn(name: "StockInstrumentoId", table: "PrestamosInstrumentos");
        }
    }
}
