using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaGestionOrquesta.Models
{
    public class Asistencia
    {
        public int AsistenciaId { get; set; }
        public Guid EstudianteId { get; set; }
        public int CursoId { get; set; }
        public DateTime Fecha { get; set; }
        public bool Presente { get; set; }

        [MaxLength(30)]
        public string? EstadoAsistencia { get; set; }

        [MaxLength(500)]
        public string? Observacion { get; set; }

        [JsonIgnore]
        public virtual Estudiante? Estudiante { get; set; }

        [JsonIgnore]
        public virtual Curso? Curso { get; set; }
    }
}