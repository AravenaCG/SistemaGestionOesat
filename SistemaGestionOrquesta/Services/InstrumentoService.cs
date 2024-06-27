using SistemaGestionOrquesta.Models.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SistemaGestionOrquesta.Models;
using SistemaGestionOrquesta.Utils;
using static ClosedXML.Excel.XLPredefinedFormat;
using SistemaGestionOrquesta.Services;

namespace SistemaGestionOrquesta.Services
{
    public class InstrumentoService : IInstrumentoService
    {
    CustomResponse<Instrumento> customResponse;
    OrquestaOESATContext _orquestaContext;

    public InstrumentoService(OrquestaOESATContext devContext) { 
            _orquestaContext = devContext;
            customResponse = new CustomResponse<Instrumento>();  // Inicializar customResponse
        }

        public async Task<CustomResponse<Instrumento>> Save(Instrumento instrumento)
        {

            Instrumento instrumentoExistente = await LinQueris.GetInstrumentoByNameAsync(_orquestaContext,instrumento.Nombre);
            if (instrumentoExistente != null)
            {
                customResponse = customResponse.BuildCustomResponse(instrumento, "Instrumento", "Error", "El instrumento ya existe en la base de datos.");
                return customResponse;
            }
            else
            {
                try
                {
                    await LinQueris.SaveInstrumento(_orquestaContext, instrumento);
                    return customResponse.BuildCustomResponse(instrumento, "Instrumento", "respuesta exitosa", "");
                }
                catch (DbUpdateException ex)
                {
                    return customResponse.HandleDbUpdateException(ex, "Instrumento", "Parámetros de entrada inválidos/mal escritos", "Violación de clave única");
                }

            }

        }

        async Task<CustomResponse<Instrumento>> IInstrumentoService.Delete(int id)
        {
            string message = string.Empty;
            try
            {
                Instrumento instrumento = await LinQueris.GetInstrumentoById(_orquestaContext, id);
                message = await LinQueris.DeleteInstrumento(_orquestaContext, instrumento);
                return customResponse.BuildCustomResponse(instrumento, "Instrumento", "respuesta exitosa",message);
                // Resto del código para manejar el mensaje
            }
            catch (DbUpdateException ex)
            {
                return customResponse.HandleDbUpdateException(ex, "Instrumento", "Parámetros de entrada inválidos/mal escritos", "Violación de clave única");
            }

            // Resto del código para manejar el mensaje
        }



    
        async Task<CustomResponse<Instrumento>> IInstrumentoService.Update(int id, Instrumento instrumentoModificado)
        {
            string message = string.Empty;
            Instrumento instrumento = await LinQueris.GetInstrumentoById(_orquestaContext, id);

            try
            {
                message = await LinQueris.ModificarInstrumentoAsync(_orquestaContext, id, instrumentoModificado);
                return customResponse.BuildCustomResponse(instrumento, "Instrumento", "respuesta exitosa", message);
                // Resto del código para manejar el mensaje
            }
            catch (DbUpdateException ex)
            {
                return customResponse.HandleDbUpdateException(ex, "Instrumento", "Parámetros de entrada inválidos/mal escritos", "Violación de clave única");
            }
        }
        List<Instrumento> IInstrumentoService.Get()
        {
           return LinQueris.GetInstrumentos(_orquestaContext);
        }

        async Task<Instrumento> IInstrumentoService.Get(int id)
        {
            Instrumento instrumento = await LinQueris.GetInstrumentoById(_orquestaContext, id);

            if (instrumento != null)
            {
                return await LinQueris.GetInstrumentoByNameAsync(_orquestaContext, instrumento.Nombre);
            }
            else
            {
                return null; // Devuelve null cuando el estudiante no existe
            }
        }

       

        public Task<Instrumento> GetInstrumentoByNombre(string nombre)
        {
            return LinQueris.GetInstrumentoByNameAsync(_orquestaContext,nombre);
        }

        public Task PrestarInstrumento(Guid idEstudiante, int idInstrumento)
        {
            return LinQueris.PrestarInstrumentoAsync(_orquestaContext,idEstudiante,idInstrumento);
        }

        public Task DevolverInstrumentoAsync(Guid idEstudiante, int idInstrumento)
        {
            return LinQueris.DevolverInstrumentoAsync(_orquestaContext, idEstudiante, idInstrumento);
        }
    }
}



public interface IInstrumentoService
{
    Task<CustomResponse<Instrumento>> Save(Instrumento instrumento);
    Task<CustomResponse<Instrumento>> Delete(int id);
    Task<CustomResponse<Instrumento>> Update(int id, Instrumento estudiante);
    
    List<Instrumento> Get();
    Task<Instrumento> Get(int id);

    Task<Instrumento> GetInstrumentoByNombre(string nombre);

    Task PrestarInstrumento(Guid idEstudiante, int idInstrumento);
    Task DevolverInstrumentoAsync(Guid idEstudiante, int idInstrumento);

}


