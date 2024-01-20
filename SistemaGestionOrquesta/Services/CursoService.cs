using SistemaGestionOrquesta.Models.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SistemaGestionOrquesta.Models;
using SistemaGestionOrquesta.Utils;
using static ClosedXML.Excel.XLPredefinedFormat;
using SistemaGestionOrquesta.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace SistemaGestionOrquesta.Services
{
    public class CursoService : ICursoService
    {
    CustomResponse<Curso> customResponse;
    OrquestaOESATContext _orquestaContext;

    public CursoService(OrquestaOESATContext devContext) { 
            _orquestaContext = devContext;
            customResponse = new CustomResponse<Curso>();  // Inicializar customResponse
        }
      
        public async Task<CustomResponse<Curso>> Save(Curso curso)
        {

            Curso cursoExistente = await LinQueris.GetCursoByNameAsync(_orquestaContext,curso.Nombre);
            if (cursoExistente != null)
            {
                customResponse = customResponse.BuildCustomResponse(curso, "Curso", "Error", "El curso ya existe en la base de datos.");
                return customResponse;
            }
            else
            {
                try
                {
                    await LinQueris.SaveCurso(_orquestaContext, curso);
                    return customResponse.BuildCustomResponse(curso, "Curso", "respuesta exitosa", "");
                }
                catch (DbUpdateException ex)
                {
                    return customResponse.HandleDbUpdateException(ex, "Curso", "Parámetros de entrada inválidos/mal escritos", "Violación de clave única");
                }

            }

        }

        async Task<CustomResponse<Curso>> ICursoService.Delete(int id)
        {
            string message = string.Empty;
            try
            {
                Curso curso = await LinQueris.GetCursoById(_orquestaContext, id);
                if (curso != null)
                message = await LinQueris.DeleteCurso(_orquestaContext, curso);
                return customResponse.BuildCustomResponse(curso, "Curso", "respuesta exitosa",message);
                // Resto del código para manejar el mensaje
            }
            catch (DbUpdateException ex)
            {
                return customResponse.HandleDbUpdateException(ex, "Curso", "Parámetros de entrada inválidos/mal escritos", "Violación de clave única");
            }

            // Resto del código para manejar el mensaje
        }

        async Task<CustomResponse<Curso>> ICursoService.Update(int id, Curso cursoModificado)
        {
            string message = string.Empty;
            Curso curso = await LinQueris.GetCursoById(_orquestaContext, id);

            try
            {
                message = await LinQueris.ModificarCursoAsync(_orquestaContext, id, cursoModificado);
                return customResponse.BuildCustomResponse(curso, "Curso", "respuesta exitosa", message);
                // Resto del código para manejar el mensaje
            }
            catch (DbUpdateException ex)
            {
                return customResponse.HandleDbUpdateException(ex, "Curso", "Parámetros de entrada inválidos/mal escritos", "Violación de clave única");
            }
        }
      
        List<Curso> ICursoService.Get()
        {
           return LinQueris.GetCursos(_orquestaContext);
        }

        async Task<Curso> ICursoService.Get(int id)
        {
            Curso curso = await LinQueris.GetCursoById(_orquestaContext, id);

            if (curso != null)
            {
                return await LinQueris.GetCursoByNameAsync(_orquestaContext, curso.Nombre);
            }
            else
            {
                return null; // Devuelve null cuando el estudiante no existe
            }
        }

        public Task<Curso> GetCursoByNombre(string nombre)
        {
            return LinQueris.GetCursoByNameAsync(_orquestaContext,nombre);
        }
    }
}



public interface ICursoService
{
    Task<CustomResponse<Curso>> Save(Curso curso);
    Task<CustomResponse<Curso>> Delete(int id);
    Task<CustomResponse<Curso>> Update(int id, Curso estudiante);
    
    List<Curso> Get();
    Task<Curso> Get(int id);

    Task<Curso> GetCursoByNombre(string nombre);
}


