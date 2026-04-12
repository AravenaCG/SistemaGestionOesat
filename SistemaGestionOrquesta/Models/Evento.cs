using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionOrquesta.Models
{
    public class Evento
    {
        [Key]
        [MaxLength(50)]
        public string EventoId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? Subtitle { get; set; }

        [Required]
        [MaxLength(30)]
        public string Type { get; set; } = string.Empty;

        [Required]
        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        [Required]
        [MaxLength(300)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Tag { get; set; }

        public string? AttendeesJson { get; set; }

        public int? CourseId { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }
    }
}
