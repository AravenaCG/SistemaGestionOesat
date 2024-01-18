using System;
using System.Collections.Generic;

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

        public virtual Profesor? Profesor { get; set; }

        public virtual ICollection<Estudiante> Estudiantes { get; set; }
    }
}
