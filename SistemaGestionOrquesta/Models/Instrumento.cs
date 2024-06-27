using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SistemaGestionOrquesta.Models
{
    public partial class Instrumento
    {
        public Instrumento()
        {
            Estudiantes = new HashSet<Estudiante>();
            PrestamosInstrumentos = new HashSet<PrestamosInstrumento>();
        }

        public int InstrumentoId { get; set; }
        public string? Nombre { get; set; }
        public string? Detalles { get; set; }
        public bool Disponible { get; set; }
        [JsonIgnore]

        public virtual ICollection<Estudiante> Estudiantes { get; set; }
        [JsonIgnore]

        public virtual ICollection<PrestamosInstrumento> PrestamosInstrumentos { get; set; }
    }
}
