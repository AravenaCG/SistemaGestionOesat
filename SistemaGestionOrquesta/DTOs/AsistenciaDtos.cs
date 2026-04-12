using System;
using System.Collections.Generic;

namespace SistemaGestionOrquesta.DTOs
{
    public class SaveAttendanceDto
    {
        public int CursoId { get; set; }
        public DateTime Fecha { get; set; }
        public List<AttendanceItemDto> Asistencias { get; set; } = new();
    }

    public class AttendanceItemDto
    {
        public Guid EstudianteId { get; set; }
        public bool Presente { get; set; }
        public string? EstadoAsistencia { get; set; }
        public string? Observacion { get; set; }
    }

    public class AsistenciaResponseDto
    {
        public int AsistenciaId { get; set; }
        public Guid EstudianteId { get; set; }
        public string? NombreEstudiante { get; set; }
        public string? ApellidoEstudiante { get; set; }
        public int CursoId { get; set; }
        public string? NombreCurso { get; set; }
        public DateTime Fecha { get; set; }
        public bool Presente { get; set; }
        public string? EstadoAsistencia { get; set; }
        public string? Observacion { get; set; }
    }
}