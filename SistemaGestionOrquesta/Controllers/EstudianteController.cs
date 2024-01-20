
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
    public class EstudianteController : ControllerBase
    {

        IEstudianteService estudianteService;


        public EstudianteController(IEstudianteService _estudianteService)
        {
            estudianteService = _estudianteService;
        }

        private IActionResult HandleCustomResponse(CustomResponse<Estudiante> customResponse)
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


        [Microsoft.AspNetCore.Mvc.HttpPost("/estudiante/save")]
        public async Task<IActionResult> Post([FromBody] Estudiante estudiante)
        {   
            var customResponse = await estudianteService.Save(estudiante);
            return HandleCustomResponse(customResponse);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("/estudiante/delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var customResponse = await estudianteService.Delete(id);
                return HandleCustomResponse(customResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("/estudiante/baja/{id}")]
        public async Task<IActionResult> Baja(Guid id)
        {
            try
            {
                var customResponse = await estudianteService.Baja(id);
                return HandleCustomResponse(customResponse);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPut("/estudiante/update/{id}")]
        public async Task<IActionResult> Edit(Guid id,[FromBody] Estudiante estudiante)
        {

            try
            {
                var customResponse = await estudianteService.Update(id, estudiante);
                return HandleCustomResponse(customResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Microsoft.AspNetCore.Mvc.HttpGet("/estudiantes")]
        public async Task<List<Estudiante>> Get()
        {
            var customResponse =  estudianteService.Get();
            return customResponse;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("/estudiante/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var customResponse = await estudianteService.Get(id);
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

        [Microsoft.AspNetCore.Mvc.HttpGet("/estudianteNombreYDni/{nombre}/{documento}")]
        public async Task<IActionResult> GetEstudiante(string nombre, string documento)
        {
            try
            {
                var customResponse = await estudianteService.GetEstudianteByNombreYDocumento(nombre, documento);
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

        [Microsoft.AspNetCore.Mvc.HttpGet("/estudianteNombreApellido/{nombre}/{apellido}")]
        public async Task<IActionResult> GetEstudianteNombreApellido(string nombre, string apellido)
        {
            try
            {
                var customResponse = await estudianteService.GetEstudianteByNombreYApellido(nombre, apellido);
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

        [Microsoft.AspNetCore.Mvc.HttpGet("/estudiantesByCurso/{idCurso}")]
        public async Task<List<Estudiante>> GetEstudiantesByCurso(int idCurso)
        {
            var customResponse = estudianteService.GetEstudiantesActivosByCurso(idCurso);
            return customResponse;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("/cursosByEstudiante/{idEstudiante}")]
        public  Task<List<Curso>> GetCursosByEstudiante(Guid idEstudiante)
        {
            var customResponse = estudianteService.GetCursosByEstudiante(idEstudiante);
            return customResponse;
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("/estudianteEliminarCurso/{id}/{Idcurso}")]
        public async Task<IActionResult> DeleteCursoEstudiante(Guid id, int Idcurso)
        {
            try
            {
                var customResponse = estudianteService.EliminarCursoEstudiante(id, Idcurso);
                return Ok(customResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("/estudianteDarDeAltaEnCurso/{id}/{Idcurso}")]
        public async Task<IActionResult> DarDeAltaEnCurso(Guid id, int Idcurso)
        {
            try
            {
                var customResponse = estudianteService.InscribirEstudianteCurso(id, Idcurso);
                return Ok(customResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Microsoft.AspNetCore.Mvc.HttpPut("/estudianteCambiarCurso/{id}/{cursoNuevo}/{cursoViejo}")]
        public async Task<IActionResult> EditCurso(Guid id, int cursoNuevo,int cursoViejo)
        {

            try
            {
                var customResponse =  estudianteService.CambiarEstudianteDeCurso(id, cursoNuevo,cursoViejo);
                return Ok(customResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
