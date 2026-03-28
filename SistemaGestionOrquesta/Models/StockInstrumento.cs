namespace SistemaGestionOrquesta.Models
{
    public partial class StockInstrumento
    {
        public int StockInstrumentoId { get; set; } // PK Autoincremental
        public string CodigoInventario { get; set; } = null!; // Ej: "CB-001"
        public string? NumeroSerie { get; set; }
        public string Estado { get; set; } = "Disponible";

        public int InstrumentoId { get; set; } // FK al catálogo (Violín, Contrabajo...)
        public virtual Instrumento Instrumento { get; set; } = null!;

        public virtual ICollection<PrestamosInstrumento> PrestamosInstrumentos { get; set; } = new List<PrestamosInstrumento>();
    }
}