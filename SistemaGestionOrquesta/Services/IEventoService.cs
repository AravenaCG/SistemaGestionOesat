using SistemaGestionOrquesta.DTOs;

namespace SistemaGestionOrquesta.Services
{
    public interface IEventoService
    {
        Task<List<CalendarEventResponseDto>> GetEventosAsync(string? month, string? type, int? courseId);
        Task<CalendarEventResponseDto> CrearEventoAsync(CreateCalendarEventDto dto);
        Task<CalendarEventResponseDto?> ActualizarEventoAsync(string id, UpdateCalendarEventDto dto);
        Task<bool> EliminarEventoAsync(string id);
    }
}
