using ExiContratos.Models.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SistemaGestionOrquesta.Models;
using SistemaGestionOrquesta.Utils;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace ExiContratos.Services
{
    public class EstudianteService : IEstudianteService
{
    CustomResponse<Estudiante> customResponse;
    OrquestaOESATContext _orquestaContext;

    public EstudianteService(OrquestaOESATContext devContext) { 
            _orquestaContext = devContext;
            customResponse = new CustomResponse<Estudiante>();  // Inicializar customResponse
        }

        public async Task<CustomResponse<Estudiante>> Save(Estudiante estudiante)
        {

            Estudiante estudianteExistente = await LinQueris.BuscarEstudiantePorNombreYDniAsync(_orquestaContext, estudiante.Nombre,estudiante.Documento);
            if (estudianteExistente != null)
            {
                customResponse = customResponse.BuildCustomResponse(estudiante, "Estudiante", "Error", "El estudiante ya existe en la base de datos.");
                return customResponse;
            }
            else
            {
                try
                {
                    LinQueris.SaveEstudiante(_orquestaContext, estudiante);
                    return customResponse.BuildCustomResponse(estudiante, "Estudiante", "respuesta exitosa", "");
                }
                catch (DbUpdateException ex)
                {
                    return customResponse.HandleDbUpdateException(ex, "Estudiante", "Parámetros de entrada inválidos/mal escritos", "Violación de clave única");
                }

            }

        }

        async Task<CustomResponse<Estudiante>> IEstudianteService.Delete(Guid id)
        {
            string message = string.Empty;
            try
            {
                Estudiante estudiante = await LinQueris.GetEstudianteById(_orquestaContext, id);
                message = await LinQueris.DeleteEstudianteAsync(_orquestaContext, estudiante);
                return customResponse.BuildCustomResponse(estudiante,"Estudiante", "respuesta exitosa",message);
                // Resto del código para manejar el mensaje
            }
            catch (DbUpdateException ex)
            {
                return customResponse.HandleDbUpdateException(ex, "Estudiante", "Parámetros de entrada inválidos/mal escritos", "Violación de clave única");
            }

            // Resto del código para manejar el mensaje
        }



        async Task<CustomResponse<Estudiante>> IEstudianteService.Baja(Guid id)
        {
            Estudiante estudiante = await LinQueris.GetEstudianteById(_orquestaContext,id);
             LinQueris.DarBajaEstudianteAsync(_orquestaContext, id);
            return customResponse.BuildCustomResponse(estudiante, "Estudiante", "respuesta exitosa", "Estudiante dado de baja exitosamente");
        }
        async Task<CustomResponse<Estudiante>> IEstudianteService.Update(Guid id, Estudiante estudianteModificado)
        {
            string message = string.Empty;
            Estudiante estudiante = await LinQueris.GetEstudianteById(_orquestaContext, id);

            try
            {
                message = await LinQueris.ModificarEstudianteAsync(_orquestaContext, id, estudianteModificado);
                return customResponse.BuildCustomResponse(estudiante, "Estudiante", "respuesta exitosa", message);
                // Resto del código para manejar el mensaje
            }
            catch (DbUpdateException ex)
            {
                return customResponse.HandleDbUpdateException(ex, "Estudiante", "Parámetros de entrada inválidos/mal escritos", "Violación de clave única");
            }
        }
        List<Estudiante> IEstudianteService.Get()
        {
           return LinQueris.GetEstudiantesActive(_orquestaContext);
        }

        async Task<Estudiante> IEstudianteService.Get(Guid id)
        {
            Estudiante estudiante = await LinQueris.GetEstudianteById(_orquestaContext, id);

            if (estudiante != null)
            {
                return await LinQueris.GetEstudianteByNameAsync(_orquestaContext, estudiante.Nombre, estudiante.Apellido);
            }
            else
            {
                return null; // Devuelve null cuando el estudiante no existe
            }
        }

        Task<Estudiante> IEstudianteService.GetEstudianteByNombreYDocumento(string nombre, string documento)
        {
            return LinQueris.BuscarEstudiantePorNombreYDniAsync(_orquestaContext, nombre, documento);
        }

        public Task<Estudiante> GetEstudianteByNombreYApellido(string nombre, string apellido)
        {
            return LinQueris.GetEstudianteByNameAsync(_orquestaContext,nombre, apellido);
        }

        public List<Estudiante> GetEstudiantesActivosByCurso(int idCurso)
        {
            return LinQueris.GetEstudiantesByCurso(_orquestaContext, idCurso);
        }

        public Task<List<Curso>> GetCursosByEstudiante(Guid estudianteId)
        {
            return LinQueris.GetCursosByEstudiante(_orquestaContext, estudianteId);
        }

        public Task CambiarEstudianteDeCurso(Guid estudianteId, int nuevoCurso, int viejoCurso)
        {
            return LinQueris.CambiarEstudianteDeCursoAsync(_orquestaContext, estudianteId,nuevoCurso,viejoCurso);
        }
    }
}



public interface IEstudianteService
{
    Task<CustomResponse<Estudiante>> Save(Estudiante estudiante);
    Task<CustomResponse<Estudiante>> Delete(Guid id);
    Task<CustomResponse<Estudiante>> Baja(Guid id);
    Task<CustomResponse<Estudiante>> Update(Guid id, Estudiante estudiante);
    
    List<Estudiante> Get();
    Task<Estudiante> Get(Guid id);
    Task<Estudiante> GetEstudianteByNombreYDocumento(string nombre, string documento);

    Task<Estudiante> GetEstudianteByNombreYApellido(string nombre, string apellido);

    List<Estudiante> GetEstudiantesActivosByCurso(int idCurso);

    Task<List<Curso>> GetCursosByEstudiante(Guid estudianteId);

    Task CambiarEstudianteDeCurso(Guid estudianteId, int viejoCurso,int nuevoCurso);
}


