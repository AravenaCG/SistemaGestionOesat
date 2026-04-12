using Microsoft.EntityFrameworkCore;
using SistemaGestionOrquesta.DTOs;
using SistemaGestionOrquesta.Models;
using System.Globalization;
using System.Text.Json;

namespace SistemaGestionOrquesta.Services
{
    public class EventoService : IEventoService
    {
        private static readonly HashSet<string> TiposValidos = new(StringComparer.OrdinalIgnoreCase)
        {
            "ensayo",
            "concierto",
            "social",
            "seccional"
        };

        private readonly OrquestaOESATContext _context;

        public EventoService(OrquestaOESATContext context)
        {
            _context = context;
        }

        public async Task<List<CalendarEventResponseDto>> GetEventosAsync(string? month, string? type, int? courseId)
        {
            var query = _context.Eventos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(month))
            {
                if (!DateTime.TryParseExact(month, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out var monthDate))
                {
                    throw new ArgumentException("month debe tener formato yyyy-MM.");
                }

                query = query.Where(e => e.Start.Year == monthDate.Year && e.Start.Month == monthDate.Month);
            }

            if (!string.IsNullOrWhiteSpace(type))
            {
                var normalizedType = NormalizeType(type);
                query = query.Where(e => e.Type == normalizedType);
            }

            if (courseId.HasValue)
            {
                query = query.Where(e => e.CourseId == courseId.Value);
            }

            var eventos = await query
                .OrderBy(e => e.Start)
                .ToListAsync();

            return eventos.Select(MapToResponse).ToList();
        }

        public async Task<CalendarEventResponseDto> CrearEventoAsync(CreateCalendarEventDto dto)
        {
            var evento = new Evento
            {
                EventoId = Guid.NewGuid().ToString(),
                Title = dto.Title,
                Subtitle = dto.Subtitle,
                Type = NormalizeType(dto.Type),
                Start = ParseIsoDate(dto.Start, nameof(dto.Start)),
                End = string.IsNullOrWhiteSpace(dto.End) ? null : ParseIsoDate(dto.End, nameof(dto.End)),
                Location = dto.Location,
                Tag = dto.Tag,
                AttendeesJson = SerializeAttendees(dto.Attendees),
                CourseId = dto.CourseId,
                Description = dto.Description
            };

            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();

            return MapToResponse(evento);
        }

        public async Task<CalendarEventResponseDto?> ActualizarEventoAsync(string id, UpdateCalendarEventDto dto)
        {
            var evento = await _context.Eventos.FirstOrDefaultAsync(e => e.EventoId == id);
            if (evento is null)
                return null;

            evento.Title = dto.Title;
            evento.Subtitle = dto.Subtitle;
            evento.Type = NormalizeType(dto.Type);
            evento.Start = ParseIsoDate(dto.Start, nameof(dto.Start));
            evento.End = string.IsNullOrWhiteSpace(dto.End) ? null : ParseIsoDate(dto.End, nameof(dto.End));
            evento.Location = dto.Location;
            evento.Tag = dto.Tag;
            evento.AttendeesJson = SerializeAttendees(dto.Attendees);
            evento.CourseId = dto.CourseId;
            evento.Description = dto.Description;

            await _context.SaveChangesAsync();
            return MapToResponse(evento);
        }

        public async Task<bool> EliminarEventoAsync(string id)
        {
            var evento = await _context.Eventos.FirstOrDefaultAsync(e => e.EventoId == id);
            if (evento is null)
                return false;

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            return true;
        }

        private static DateTime ParseIsoDate(string? value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{fieldName} es requerido.");

            if (!DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var parsed))
                throw new ArgumentException($"{fieldName} debe ser ISO 8601 válido.");

            return parsed;
        }

        private static string NormalizeType(string? type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("type es requerido.");

            var normalized = type.Trim().ToLowerInvariant();
            if (!TiposValidos.Contains(normalized))
                throw new ArgumentException("type inválido. Valores permitidos: ensayo, concierto, social, seccional.");

            return normalized;
        }

        private static string? SerializeAttendees(List<string>? attendees)
        {
            if (attendees is null)
                return null;

            return JsonSerializer.Serialize(attendees);
        }

        private static List<string>? DeserializeAttendees(string? attendeesJson)
        {
            if (string.IsNullOrWhiteSpace(attendeesJson))
                return new List<string>();

            try
            {
                return JsonSerializer.Deserialize<List<string>>(attendeesJson) ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        private static CalendarEventResponseDto MapToResponse(Evento evento)
        {
            return new CalendarEventResponseDto
            {
                Id = evento.EventoId,
                Title = evento.Title,
                Subtitle = evento.Subtitle,
                Type = evento.Type,
                Start = evento.Start.ToString("yyyy-MM-ddTHH:mm:ss"),
                End = evento.End?.ToString("yyyy-MM-ddTHH:mm:ss"),
                Location = evento.Location,
                Tag = evento.Tag,
                Attendees = DeserializeAttendees(evento.AttendeesJson),
                CourseId = evento.CourseId,
                Description = evento.Description
            };
        }
    }
}
