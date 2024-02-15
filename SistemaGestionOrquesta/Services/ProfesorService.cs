using SistemaGestionOrquesta.Models.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SistemaGestionOrquesta.Models;
using SistemaGestionOrquesta.Utils;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace SistemaGestionOrquesta.Services
{
    public class ProfesorService : IProfesorService
    {
        CustomResponse<Profesor> customResponse;
        OrquestaOESATContext _orquestaContext;

        public ProfesorService(OrquestaOESATContext devContext)
        {
            _orquestaContext = devContext;
            customResponse = new CustomResponse<Profesor>();  // Inicializar customResponse
        }

        public async Task<CustomResponse<Profesor>> Save(Profesor profesor)
        {

            Profesor profesorExistente = await LinQueris.GetProfesorPorNombreYDniAsync(_orquestaContext, profesor.Nombre, profesor.Documento);
            if (profesorExistente != null)
            {
                customResponse = customResponse.BuildCustomResponse(profesor, "Profesor", "Error", "El Profesor ya existe en la base de datos.");
                return customResponse;
            }
            else
            {
                try
                {
                    await LinQueris.SaveProfesor(_orquestaContext, profesor);
                    return customResponse.BuildCustomResponse(profesor, "Profesor", "respuesta exitosa", "");
                }
                catch (DbUpdateException ex)
                {
                    return customResponse.HandleDbUpdateException(ex, "Profesor", "Parámetros de entrada inválidos/mal escritos", "Violación de clave única");
                }

            }

        }

        async Task<CustomResponse<Profesor>> IProfesorService.Delete(Guid id)
        {
            string message = string.Empty;
            try
            {
                Profesor profesor = await LinQueris.GetProfesorById(_orquestaContext, id);
                message = await LinQueris.DeleteProfesor(_orquestaContext, profesor);
                return customResponse.BuildCustomResponse(profesor, "Profesor", "respuesta exitosa", message);
                // Resto del código para manejar el mensaje
            }
            catch (DbUpdateException ex)
            {
                return customResponse.HandleDbUpdateException(ex, "Profesor", "Parámetros de entrada inválidos/mal escritos", "Violación de clave única");
            }

            // Resto del código para manejar el mensaje
        }



        async Task<CustomResponse<Profesor>> IProfesorService.Baja(Guid id)
        {
            Profesor profesor = await LinQueris.GetProfesorById(_orquestaContext, id);
            LinQueris.DarBajaProfesorAsync(_orquestaContext, id);
            return customResponse.BuildCustomResponse(profesor, "Profesor", "respuesta exitosa", "Estudiante dado de baja exitosamente");
        }
        async Task<CustomResponse<Profesor>> IProfesorService.Update(Guid id, Profesor profesorModificado)
        {
            string message = string.Empty;
            Profesor profesor = await LinQueris.GetProfesorById(_orquestaContext, id);

            try
            {
                message = await LinQueris.ModificarProfesorAsync(_orquestaContext, id, profesorModificado);
                return customResponse.BuildCustomResponse(profesor, "Profesor", "respuesta exitosa", message);
                // Resto del código para manejar el mensaje
            }
            catch (DbUpdateException ex)
            {
                return customResponse.HandleDbUpdateException(ex, "Profesor", "Parámetros de entrada inválidos/mal escritos", "Violación de clave única");
            }
        }

        List<Profesor> IProfesorService.Get()
        {
            return LinQueris.GetProfesoresActive(_orquestaContext);
        }

        async Task<Profesor> IProfesorService.Get(Guid id)
        {
            Profesor profesor = await LinQueris.GetProfesorById(_orquestaContext, id);

            if (profesor != null)
            {
                return await LinQueris.GetProfesorByNameAsync(_orquestaContext, profesor.Nombre, profesor.Apellido);
            }
            else
            {
                return null; // Devuelve null cuando el estudiante no existe
            }
        }

        Task<Profesor> IProfesorService.GetProfesorByNombreYDocumento(string nombre, string documento)
        {
            return LinQueris.GetProfesorPorNombreYDniAsync(_orquestaContext, nombre, documento);
        }

        public Task<Profesor> GetProfesorByNombreYApellido(string nombre, string apellido)
        {
            return LinQueris.GetProfesorByNameAsync(_orquestaContext, nombre, apellido);
        }

    }
}



public interface IProfesorService
{
    Task<CustomResponse<Profesor>> Save(Profesor profesor);
    Task<CustomResponse<Profesor>> Delete(Guid id);
    Task<CustomResponse<Profesor>> Baja(Guid id);
    Task<CustomResponse<Profesor>> Update(Guid id, Profesor profesor);

    List<Profesor> Get();
    Task<Profesor> Get(Guid id);
    Task<Profesor> GetProfesorByNombreYDocumento(string nombre, string documento);

    Task<Profesor> GetProfesorByNombreYApellido(string nombre, string apellido);


}


