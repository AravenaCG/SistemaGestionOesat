using System;
using System.Collections.Generic;

namespace SistemaGestionOrquesta.Models
{
    public partial class PrestamosInstrumento
    {
        public int PrestamoInstrumentoId { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public int InstrumentoId { get; set; }
        public Guid EstudianteId { get; set; }

        public virtual Estudiante Estudiante { get; set; } = null!;
        public virtual Instrumento Instrumento { get; set; } = null!;
    }
}
