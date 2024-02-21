
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
    public class CursoController : ControllerBase
    {

        ICursoService cursoService;


        public CursoController(ICursoService _cursoService)
        {
            cursoService = _cursoService;
        }

        private IActionResult HandleCustomResponse(CustomResponse<Curso> customResponse)
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


        [Microsoft.AspNetCore.Mvc.HttpPost("/curso/save")]
        public async Task<IActionResult> Post([FromBody] Curso curso)
        {
            var customResponse = await cursoService.Save(curso);
            return HandleCustomResponse(customResponse);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("/curso/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var customResponse = await cursoService.Delete(id);
                return HandleCustomResponse(customResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }


        [Microsoft.AspNetCore.Mvc.HttpPut("/curso/update/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Curso curso)
        {

            try
            {
                var customResponse = await cursoService.Update(id, curso);
                return HandleCustomResponse(customResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Microsoft.AspNetCore.Mvc.HttpGet("/cursos")]
        public async Task<List<Curso>> Get()
        {
            var customResponse =  cursoService.Get();
            return customResponse;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("/curso/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var customResponse = await cursoService.Get(id);
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

        [Microsoft.AspNetCore.Mvc.HttpGet("/cursoNombre/{nombre}")]
        public async Task<Curso> GetCursoNombre(string nombre)
        {
            try
            {
                var customResponse = await cursoService.GetCursoByNombre(nombre);
                if (customResponse != null)
                {
                    return customResponse; 
                }
                else
                {
                
                    throw new Exception("El curso especificado no fue encontrado.");
                }
            }
            catch (Exception ex)
            {
                // Puedes manejar el error de acuerdo a tus necesidades
                throw new Exception("Error al obtener el curso: " + ex.Message);
            }
        }



    }
}
