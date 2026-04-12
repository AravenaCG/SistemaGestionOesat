using Microsoft.AspNetCore.Mvc;
using SistemaGestionOrquesta.DTOs;
using SistemaGestionOrquesta.Services;

namespace SistemaGestionOrquesta.Controllers
{
    [ApiController]
    [Route("")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet("eventos")]
        public async Task<IActionResult> GetEventos([FromQuery] string? month, [FromQuery] string? type, [FromQuery] int? courseId)
        {
            try
            {
                var eventos = await _eventoService.GetEventosAsync(month, type, courseId);
                return Ok(eventos);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("evento/save")]
        public async Task<IActionResult> SaveEvento([FromBody] CreateCalendarEventDto dto)
        {
            try
            {
                var evento = await _eventoService.CrearEventoAsync(dto);
                return StatusCode(StatusCodes.Status201Created, evento);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("evento/update/{id}")]
        public async Task<IActionResult> UpdateEvento(string id, [FromBody] UpdateCalendarEventDto dto)
        {
            try
            {
                var evento = await _eventoService.ActualizarEventoAsync(id, dto);
                if (evento is null)
                    return NotFound();

                return Ok(evento);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("evento/delete/{id}")]
        public async Task<IActionResult> DeleteEvento(string id)
        {
            var deleted = await _eventoService.EliminarEventoAsync(id);
            if (!deleted)
                return NotFound();

            return Ok();
        }
    }
}
