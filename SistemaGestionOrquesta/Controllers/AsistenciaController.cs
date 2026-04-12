using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionOrquesta.DTOs;
using SistemaGestionOrquesta.Services;
using System;
using System.Threading.Tasks;

namespace SistemaGestionOrquesta.Controllers
{
    [ApiController]
    [Route("api/asistencia")]
    public class AsistenciaController : ControllerBase
    {
        private readonly IAsistenciaService _asistenciaService;

        public AsistenciaController(IAsistenciaService asistenciaService)
        {
            _asistenciaService = asistenciaService;
        }

        // POST api/asistencia
        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] SaveAttendanceDto dto)
        {
            try
            {
                var resultado = await _asistenciaService.GuardarAsistenciaAsync(dto);
                return StatusCode(StatusCodes.Status201Created, resultado);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/asistencia?cursoId=10&fecha=2024-04-05
        [HttpGet]
        public async Task<IActionResult> GetByCursoYFecha([FromQuery] int cursoId, [FromQuery] DateTime fecha)
        {
            try
            {
                var resultado = await _asistenciaService.GetByCursoYFechaAsync(cursoId, fecha);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/asistencia/estudiante/{estudianteId}
        [HttpGet("estudiante/{estudianteId}")]
        public async Task<IActionResult> GetByEstudiante(Guid estudianteId)
        {
            try
            {
                var resultado = await _asistenciaService.GetByEstudianteAsync(estudianteId);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}