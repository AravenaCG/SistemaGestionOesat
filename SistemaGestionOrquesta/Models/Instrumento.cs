using System;
using System.Collections.Generic;

namespace SistemaGestionOrquesta.Models
{
    public partial class Instrumento
    {
        public Instrumento()
        {
            Estudiantes = new HashSet<Estudiante>();
        }

        public int InstrumentoId { get; set; }
        public string? Nombre { get; set; }
        public string? Detalles { get; set; }

        // Nueva propiedad para rastrear el estado del préstamo
        public bool Disponible { get; set; } = true;

        public virtual ICollection<Estudiante> Estudiantes { get; set; }
    }

}
