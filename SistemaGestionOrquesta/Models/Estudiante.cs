using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SistemaGestionOrquesta.Models
{
    public partial class Estudiante
    {
        public Estudiante()
        {
            PrestamosInstrumentos = new HashSet<PrestamosInstrumento>();
            Cursos = new HashSet<Curso>();
        }

        public Guid EstudianteId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Documento { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Email { get; set; }
        public int? InstrumentoId { get; set; }
        public string? RutaFoto { get; set; }
        public bool? Activo { get; set; }
        public string? NombreTutor { get; set; }
        public string? TelefonoTutor { get; set; }
        public bool? Asegurado { get; set; }
        public string? DocumentoTutor { get; set; }
        public string? DocumentoTutor2 { get; set; }
        public string? NombreTutor2 { get; set; }
        public string? TelefonoTutor2 { get; set; }
        public string? Nacionalidad { get; set; }
        public string? TmtMédico { get; set; }
        public string? EpPsicoMotriz { get; set; }
        public string? Particularidad { get; set; }
        public bool? Autoretiro { get; set; }


        public virtual Instrumento? Instrumento { get; set; }


        public virtual ICollection<PrestamosInstrumento> PrestamosInstrumentos { get; set; }


        public virtual ICollection<Curso> Cursos { get; set; }
    }
}
