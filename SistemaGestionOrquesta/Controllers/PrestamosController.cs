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
        private readonly PrestamosService _prestamosService;

        public PrestamosController(PrestamosService prestamosService)
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
    }
}