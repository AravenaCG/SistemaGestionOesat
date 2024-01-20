
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
    public class ProfesorController : ControllerBase
    {

        IProfesorService profesorService;


        public ProfesorController(IProfesorService _profesorService)
        {
            profesorService = _profesorService;
        }

        private IActionResult HandleCustomResponse(CustomResponse<Profesor> customResponse)
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


        [Microsoft.AspNetCore.Mvc.HttpPost("/profesor/save")]
        public async Task<IActionResult> Post([FromBody] Profesor profesor)
        {   
            var customResponse = await profesorService.Save(profesor);
            return HandleCustomResponse(customResponse);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("/profesor/delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var customResponse = await profesorService.Delete(id);
                return HandleCustomResponse(customResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("/profesor/baja/{id}")]
        public async Task<IActionResult> Baja(Guid id)
        {
            try
            {
                var customResponse = await profesorService.Baja(id);
                return HandleCustomResponse(customResponse);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPut("/profesor/update/{id}")]
        public async Task<IActionResult> Edit(Guid id,[FromBody] Profesor profesor)
        {

            try
            {
                var customResponse = await profesorService.Update(id, profesor);
                return HandleCustomResponse(customResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Microsoft.AspNetCore.Mvc.HttpGet("/profesores")]
        public async Task<List<Profesor>> Get()
        {
            var customResponse = profesorService.Get();
            return customResponse;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("/profesor/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var customResponse = await profesorService.Get(id);
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

        [Microsoft.AspNetCore.Mvc.HttpGet("/profesorNombreYDni/{nombre}/{documento}")]
        public async Task<IActionResult> GetProfesor(string nombre, string documento)
        {
            try
            {
                var customResponse = await profesorService.GetProfesorByNombreYDocumento(nombre, documento);
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

        [Microsoft.AspNetCore.Mvc.HttpGet("/profesorNombreApellido/{nombre}/{apellido}")]
        public async Task<IActionResult> GetProfesorNombreApellido(string nombre, string apellido)
        {
            try
            {
                var customResponse = await profesorService.GetProfesorByNombreYApellido(nombre, apellido);
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

     
    }
}
