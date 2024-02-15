using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SistemaGestionOrquesta.Models
{
    public partial class Profesor
    {
        public Profesor()
        {
            Cursos = new HashSet<Curso>();
        }

        public Guid ProfesorId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Documento { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Email { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<Curso> Cursos { get; set; }
    }
}
