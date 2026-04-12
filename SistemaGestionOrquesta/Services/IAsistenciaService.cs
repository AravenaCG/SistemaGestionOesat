using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaGestionOrquesta.DTOs;

namespace SistemaGestionOrquesta.Services
{
    public interface IAsistenciaService
    {
        Task<List<AsistenciaResponseDto>> GuardarAsistenciaAsync(SaveAttendanceDto dto);
        Task<List<AsistenciaResponseDto>> GetByCursoYFechaAsync(int cursoId, DateTime fecha);
        Task<List<AsistenciaResponseDto>> GetByEstudianteAsync(Guid estudianteId);
    }
}