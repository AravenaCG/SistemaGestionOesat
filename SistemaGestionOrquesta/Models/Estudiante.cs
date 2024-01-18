using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SistemaGestionOrquesta.Models
{
    public partial class Estudiante
    {
        public Estudiante()
        {
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
        public string? NombreTutor2 { get; set; }
        public string? DocumentoTutor2 { get; set; }
        public string? TelefonoTutor2 { get; set; }


        [JsonIgnore]

        public virtual Instrumento? Instrumento { get; set; }
        [JsonIgnore]

        public virtual ICollection<Curso>? Cursos { get; set; }

        // Nueva propiedad para el préstamo de instrumentos
        [JsonIgnore]

        public virtual ICollection<PrestamoInstrumento>? Prestamos { get; set; }
    }

}
