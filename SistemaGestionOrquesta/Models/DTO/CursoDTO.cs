using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SistemaGestionOrquesta.Models
{
    public partial class CursoDTO
    {
        public CursoDTO()
        {
            Estudiantes = new HashSet<Estudiante>();
        }

        public int CursoId { get; set; }
        public string? Nombre { get; set; }
        public Guid? ProfesorId { get; set; }

        public virtual Profesor? Profesor { get; set; }

        [JsonIgnore]
        public virtual ICollection<Estudiante> Estudiantes { get; set; }
    }
}
