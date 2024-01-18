using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionOrquesta.Models
{
    public class PrestamoInstrumento
    {
        public int PrestamoInstrumentoId { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }

        public int InstrumentoId { get; set; }
        public virtual Instrumento Instrumento { get; set; }

        public Guid EstudianteId { get; set; }
        public virtual Estudiante Estudiante { get; set; }
    }
}
