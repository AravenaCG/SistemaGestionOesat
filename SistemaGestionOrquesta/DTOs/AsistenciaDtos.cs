using System;
using System.Collections.Generic;

namespace SistemaGestionOrquesta.DTOs
{
    public record SaveAttendanceDto(
        int CursoId,
        DateTime Fecha,
        List<AttendanceItemDto> Asistencias
    );

    public record AttendanceItemDto(
        Guid EstudianteId,
        bool Presente
    );

    public record AsistenciaResponseDto(
        int AsistenciaId,
        Guid EstudianteId,
        string? NombreEstudiante,
        string? ApellidoEstudiante,
        int CursoId,
        string? NombreCurso,
        DateTime Fecha,
        bool Presente
    );
}