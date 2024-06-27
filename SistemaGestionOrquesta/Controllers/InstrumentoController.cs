
using SistemaGestionOrquesta.Models.Middleware;
using SistemaGestionOrquesta.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SistemaGestionOrquesta.Models;
using SistemaGestionOrquesta.Utils;
using System.Threading;
using System.Web.Mvc;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;

namespace SistemaGestionOrquesta.Controllers
{

    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class InstrumentoController : ControllerBase
    {

        IInstrumentoService instrumentoService;


        public InstrumentoController(IInstrumentoService _instrumentoService)
        {
            instrumentoService = _instrumentoService;
        }

        private IActionResult HandleCustomResponse(CustomResponse<Instrumento> customResponse)
        {
            if (customResponse.Messages.Any(message => message.status == "respuesta exitosa"))
            {
                return StatusCode(StatusCodes.Status201Created, customResponse.JsonResult);
            }
            else if (customResponse.Messages.Any(message => message.status == "Parámetros de entrada inválidos/mal escritos"))
            {
                return BadRequest(customResponse);
            }
            else if (customResponse.Messages.Any(message => message.status == "Timeout"))
            {
                return StatusCode(StatusCodes.Status504GatewayTimeout, customResponse.JsonResult);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, customResponse.JsonResult);
        }


        [Microsoft.AspNetCore.Mvc.HttpPost("/instrumento/save")]
        public async Task<IActionResult> Post([FromBody] Instrumento instrumento)
        {   
            var customResponse = await instrumentoService.Save(instrumento);
            return HandleCustomResponse(customResponse);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("/instrumento/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var customResponse = await instrumentoService.Delete(id);
                return HandleCustomResponse(customResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            
        }


        [Microsoft.AspNetCore.Mvc.HttpPut("/instrumento/update/{id}")]
        public async Task<IActionResult> Edit(int id,[FromBody] Instrumento instrumento)
        {

            try
            {
                var customResponse = await instrumentoService.Update(id, instrumento);
                return HandleCustomResponse(customResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Microsoft.AspNetCore.Mvc.HttpGet("/instrumentos")]
        public async Task<List<Instrumento>> Get()
        {
            var customResponse = instrumentoService.Get();
            return customResponse;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("/instrumento/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var customResponse = await instrumentoService.Get(id);
                if(customResponse != null) { 
                return Ok(customResponse);
                     }
                else { return BadRequest(); }

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("/instrumentoNombre/{nombre}")]
        public async Task<IActionResult> GetProfesor(string nombre)
        {
            try
            {
                var customResponse = await instrumentoService.GetInstrumentoByNombre(nombre);
                if (customResponse != null)
                {
                    return Ok(customResponse);
                }
                else { return BadRequest(); }

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("/instrumento/prestamo/{idEstudiante}/{idInstrumento}")]
        public async Task<IActionResult> PostPrestamo( Guid idEstudiante, int idInstrumento)
        {
             await instrumentoService.PrestarInstrumento(idEstudiante, idInstrumento);
            return Ok();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("/instrumento/devolucion/{idEstudiante}/{idInstrumento}")]
        public async Task<IActionResult> PostDevolucion(Guid idEstudiante, int idInstrumento)
        {
             await instrumentoService.DevolverInstrumentoAsync(idEstudiante, idInstrumento);
            return Ok();
        }

    }
}
