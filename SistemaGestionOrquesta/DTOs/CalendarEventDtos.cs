using System.Collections.Generic;

namespace SistemaGestionOrquesta.DTOs
{
    public class CreateCalendarEventDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Start { get; set; } = string.Empty;
        public string? End { get; set; }
        public string Location { get; set; } = string.Empty;
        public string? Tag { get; set; }
        public List<string>? Attendees { get; set; }
        public int? CourseId { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateCalendarEventDto : CreateCalendarEventDto
    {
        public string Id { get; set; } = string.Empty;
    }

    public class CalendarEventResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Start { get; set; } = string.Empty;
        public string? End { get; set; }
        public string Location { get; set; } = string.Empty;
        public string? Tag { get; set; }
        public List<string>? Attendees { get; set; }
        public int? CourseId { get; set; }
        public string? Description { get; set; }
    }
}
