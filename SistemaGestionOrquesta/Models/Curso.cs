using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SistemaGestionOrquesta.Models
{
    public partial class Curso
    {
        public Curso()
        {
            Estudiantes = new HashSet<Estudiante>();
        }

        public int CursoId { get; set; }
        public string? Nombre { get; set; }
        public Guid? ProfesorId { get; set; }
        [JsonIgnore]
        public virtual Profesor? Profesor { get; set; }
        [JsonIgnore]

        public virtual ICollection<Estudiante> Estudiantes { get; set; }
    }
}
