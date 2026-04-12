using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SistemaGestionOrquesta.Services;
using SistemaGestionOrquesta.DTOs;
using System;

namespace SistemaGestionOrquesta.Controllers
{
    [ApiController]
    [Route("api/prestamos")]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamosService _prestamosService;

        public PrestamosController(IPrestamosService prestamosService)
        {
            _prestamosService = prestamosService;
        }

        [HttpPost("asignar")]
        public async Task<IActionResult> Asignar([FromBody] AsignarPrestamoDto dto)
        {
            try
            {
                var prestamo = await _prestamosService.AsignarPrestamoAsync(dto);
                return Ok(prestamo);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
            catch (InvalidOperationException ioe)
            {
                return Conflict(ioe.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("devolver")]
        public async Task<IActionResult> Devolver([FromBody] DevolverPrestamoDto dto)
        {
            try
            {
                var prestamo = await _prestamosService.DevolverPrestamoAsync(dto);
                return Ok(prestamo);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ioe.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _prestamosService.GetAllAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("activos")]
        public async Task<IActionResult> GetActivos()
        {
            try
            {
                var list = await _prestamosService.GetActivosAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("estudiante/{estudianteId}")]
        public async Task<IActionResult> GetByEstudiante(Guid estudianteId)
        {
            try
            {
                var list = await _prestamosService.GetByEstudianteAsync(estudianteId);
                return Ok(list);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, traceId = HttpContext.TraceIdentifier });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var prestamo = await _prestamosService.GetByIdAsync(id);
                if (prestamo is null) return NotFound();
                return Ok(prestamo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}