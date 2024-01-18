using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SistemaGestionOrquesta.Models;

namespace SistemaGestionOrquesta.Utils
{
    internal class LinQueris
    {

        #region ABM_Estudiante

        public static async Task<int> SaveEstudiante(OrquestaOESATContext context, Estudiante estudianteToSave)
        {
            try
            {
                Estudiante estudianteExiste = await BuscarEstudiantePorNombreYDniAsync(context, estudianteToSave);

                if (estudianteExiste != null)
                {
                    Console.WriteLine($"El estudiante {estudianteExiste.Nombre} ya existe en la DB!");
                }
                else
                {
                    context.Estudiantes.Add(estudianteToSave);
                    return await context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Manejar excepción de concurrencia
                Console.WriteLine($"Error de concurrencia: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                // Manejar excepción de actualización de base de datos
                Console.WriteLine($"Error al actualizar la base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }

            // Devolver 0 en caso de error
            return 0;
        }

        public static async Task<string> DeleteEstudianteAsync(OrquestaOESATContext context, Estudiante estudianteToDelete)
        {
            try
            {
                // Intenta eliminar el estudiante
                context.Remove(estudianteToDelete);
                await context.SaveChangesAsync();
                return "Estudiante eliminado exitosamente!";
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Manejar excepción de concurrencia
                return $"Error de concurrencia: {ex.Message}";
            }
            catch (DbUpdateException ex)
            {
                // Manejar excepción de actualización de base de datos
                return $"Error al actualizar la base de datos: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                return $"Error inesperado: {ex.Message}";
            }
        }




        public static async Task DarBajaEstudianteAsync(OrquestaOESATContext context, Guid estudianteId)
        {
            var estudiante = await context.Estudiantes.FindAsync(estudianteId);

            if (estudiante != null)
            {
                estudiante.Activo = false; // Establecer el campo Activo como false para dar la baja lógica
                await context.SaveChangesAsync(); // Guardar los cambios en la base de datos
            }
        }

        public static async Task<string> ModificarEstudianteAsync(OrquestaOESATContext context, Guid estudianteId, Estudiante estudianteModificado)
        {
            var estudianteExistente = await context.Estudiantes.FindAsync(estudianteId);

            if (estudianteExistente != null)
            {
                // Actualizar propiedades según tu lógica de modificación
                estudianteExistente.Nombre = estudianteModificado.Nombre ?? estudianteExistente.Nombre;
                estudianteExistente.Apellido = estudianteModificado.Apellido ?? estudianteExistente.Apellido;
                estudianteExistente.FechaNacimiento = estudianteModificado.FechaNacimiento ?? estudianteExistente.FechaNacimiento;
                estudianteExistente.Documento = estudianteModificado.Documento ?? estudianteExistente.Documento;
                estudianteExistente.Telefono = estudianteModificado.Telefono ?? estudianteExistente.Telefono;
                estudianteExistente.Direccion = estudianteModificado.Direccion ?? estudianteExistente.Direccion;
                estudianteExistente.Email = estudianteModificado.Email ?? estudianteExistente.Email;
                estudianteExistente.InstrumentoId = estudianteModificado.InstrumentoId ?? estudianteExistente.InstrumentoId;
                estudianteExistente.RutaFoto = estudianteModificado.RutaFoto ?? estudianteExistente.RutaFoto;
                estudianteExistente.Activo = estudianteModificado.Activo ?? estudianteExistente.Activo;
                estudianteExistente.NombreTutor = estudianteModificado.NombreTutor ?? estudianteExistente.NombreTutor;
                estudianteExistente.TelefonoTutor = estudianteModificado.TelefonoTutor ?? estudianteExistente.TelefonoTutor;
                estudianteExistente.Asegurado = estudianteModificado.Asegurado ?? estudianteExistente.Asegurado;

                await context.SaveChangesAsync();
                return "Estudiante Modificado Exitosamente!";
            }
            else { return "El estudiante al parecer no existe en la DB"; }
        }


        public static async Task<Estudiante> GetEstudianteById(OrquestaOESATContext context, Guid estudianteId)
        {
            return await context.Estudiantes.FirstOrDefaultAsync(p => p.EstudianteId == estudianteId);

        }

        public static async Task<Estudiante> BuscarEstudiantePorNombreYDniAsync(OrquestaOESATContext context, Estudiante estudiante)
        {
            return await context.Estudiantes.FirstOrDefaultAsync(p => p.Nombre == estudiante.Nombre && p.Apellido == estudiante.Apellido && p.Documento == estudiante.Documento);
        }

        public static async Task<Estudiante> GetEstudianteByNameAsync(OrquestaOESATContext context, string nombreEstudiante, string apellidoEstudiante)
        {
            return await context.Estudiantes.FirstOrDefaultAsync(p => p.Nombre == nombreEstudiante && p.Apellido == apellidoEstudiante);
        }

        public static List<Estudiante> GetEstudiantesActive(OrquestaOESATContext context)
        {
            var activeCountries = context.Estudiantes.Where(p => p.Activo == true).ToList();
            return activeCountries;
        }

        public static List<Estudiante> GetEstudiantesByCurso(OrquestaOESATContext context, int idCurso)
        {
            int cursoId = 1; // Reemplaza con el ID del curso que deseas consultar

            var estudiantesActivos = context.Cursos
                .Where(curso => curso.CursoId == idCurso)
                .SelectMany(curso => curso.Estudiantes.Where(estudiante => (bool)estudiante.Activo))
                .ToList();

            // Hacer algo con la lista de estudiantes activos...
            foreach (var estudiante in estudiantesActivos)
            {
                Console.WriteLine($"Nombre del estudiante: {estudiante.Nombre}, Apellido: {estudiante.Apellido}");
            }
            return estudiantesActivos;
        }


        public static async Task<List<Curso>> GetCursosByEstudiante(OrquestaOESATContext context, Guid estudianteId)
        {
            try
            {
                // Cargar un estudiante por su ID
                var estudiante = await context.Estudiantes
                    .Include(e => e.Cursos)
                    .FirstOrDefaultAsync(e => e.EstudianteId == estudianteId);

                if (estudiante != null)
                {
                    ICollection<Curso> cursosInscritos = estudiante.Cursos;
                    // Devolver la lista de cursos inscritos
                    return cursosInscritos.ToList();
                }
                else
                {
                    // El estudiante no existe, puedes devolver null o una lista vacía según tus necesidades
                    Console.WriteLine($"No se encontró el estudiante con ID: {estudianteId}");
                    return new List<Curso>();
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción adecuadamente, ya sea registrándola o lanzándola nuevamente según tus necesidades
                Console.WriteLine($"Error al obtener cursos por estudiante: {ex.Message}");
                throw;
            }
        }


        public static async Task CambiarEstudianteDeCursoAsync(OrquestaOESATContext context, Guid estudianteId, int nuevoCursoId, int viejoCursoId)
        {
            try
            {
                // Cargar el estudiante por su ID
                var estudiante = await context.Estudiantes
                    .Include(e => e.Cursos)
                    .FirstOrDefaultAsync(e => e.EstudianteId == estudianteId);

                if (estudiante != null)
                {
                    // Verificar si el nuevo curso y el curso antiguo existen
                    var nuevoCurso = await context.Cursos.FindAsync(nuevoCursoId);
                    var viejoCurso = await context.Cursos.FindAsync(viejoCursoId);

                    if (nuevoCurso != null && viejoCurso != null)
                    {
                        // Quitar el curso antiguo de la lista de cursos del estudiante
                        estudiante.Cursos.Remove(viejoCurso);

                        // Agregar el nuevo curso a la lista de cursos del estudiante
                        estudiante.Cursos.Add(nuevoCurso);

                        // Guardar los cambios en la base de datos
                        await context.SaveChangesAsync();

                        Console.WriteLine($"Estudiante {estudiante.Nombre} cambió de {viejoCurso.Nombre} a {nuevoCurso.Nombre}");
                    }
                    else
                    {
                        Console.WriteLine($"El nuevo curso con ID {nuevoCursoId} o el curso antiguo con ID {viejoCursoId} no existe");
                    }
                }
                else
                {
                    Console.WriteLine($"No se encontró el estudiante con ID: {estudianteId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar estudiante de curso: {ex.Message}");
                throw;
            }
        }


        #endregion



        #region PROFESOR

        public static async Task<int> SaveProfesor(OrquestaOESATContext context, Profesor profesorToSave)
        {
            Profesor profesorExiste = await GetProfesorByNameAsync(context, profesorToSave.Nombre, profesorToSave.Apellido);
            if (profesorExiste != null)
            {
                Console.WriteLine("El profesor " + profesorExiste + " ya existe en la DB!");
            }
            else
            {
                context.Profesors.Add(profesorToSave);
            }
            return await context.SaveChangesAsync();
        }

        public static async Task<int> DeleteProfesor(OrquestaOESATContext context, Profesor profesorToDelete)
        {
            Profesor profesorExiste = await GetProfesorByNameAsync(context, profesorToDelete.Nombre, profesorToDelete.Apellido);
            if (profesorExiste != null)
            {
                context.Remove(profesorToDelete);
            }
            else
            {
                profesorExiste = await GetProfesorById(context, profesorToDelete.ProfesorId);
                if (profesorExiste != null)
                {
                    context.Remove(profesorToDelete);
                }
                else
                {
                    Console.WriteLine("El profesor no existe");
                }
            }
            return await context.SaveChangesAsync();
        }

        public static async Task DarBajaProfesorAsync(OrquestaOESATContext context, Guid profesorId)
        {
            var profesor = await context.Profesors.FindAsync(profesorId);

            if (profesor != null)
            {
                profesor.Activo = false; // Establecer el campo Activo como false para dar la baja lógica
                await context.SaveChangesAsync(); // Guardar los cambios en la base de datos
            }
        }

        public static async Task ModificarProfesorAsync(OrquestaOESATContext context, Guid profesorId, Profesor profesorModificado)
        {
            var profesorExistente = await context.Profesors.FindAsync(profesorId);

            if (profesorExistente != null)
            {
                // Actualizar propiedades según tu lógica de modificación
                profesorExistente.Nombre = profesorModificado.Nombre ?? profesorExistente.Nombre;
                profesorExistente.Apellido = profesorModificado.Apellido ?? profesorExistente.Apellido;
                profesorExistente.FechaNacimiento = profesorModificado.FechaNacimiento ?? profesorExistente.FechaNacimiento;
                profesorExistente.Documento = profesorModificado.Documento ?? profesorExistente.Documento;
                profesorExistente.Telefono = profesorModificado.Telefono ?? profesorExistente.Telefono;
                profesorExistente.Direccion = profesorModificado.Direccion ?? profesorExistente.Direccion;
                profesorExistente.Email = profesorModificado.Email ?? profesorExistente.Email;
                profesorExistente.Activo = profesorModificado.Activo ?? profesorExistente.Activo;

                await context.SaveChangesAsync();
            }
        }

        private static async Task<Profesor?> GetProfesorById(OrquestaOESATContext context, Guid profesorId)
        {
            return await context.Profesors.FirstOrDefaultAsync(p => p.ProfesorId == profesorId);
        }

        public static async Task<Profesor> GetProfesorByNameAsync(OrquestaOESATContext context, string nombreProfesor, string apellidoProfesor)
        {
            return await context.Profesors.FirstOrDefaultAsync(p => p.Nombre == nombreProfesor && p.Apellido == apellidoProfesor);
        }

        public static List<Profesor> GetProfesoresActive(OrquestaOESATContext context)
        {
            var profesoresActivos = context.Profesors.Where(p => p.Activo == true).ToList();
            return profesoresActivos;
        }

        public static async Task<Profesor> GeProfesorByNameAsync(OrquestaOESATContext context, string nombreProfesor)
        {
            return await context.Profesors.FirstOrDefaultAsync(p => p.Nombre == nombreProfesor);
        }

        #endregion

        #region Instrumento
        public static async Task<int> SaveInstrumento(OrquestaOESATContext context, Instrumento instrumentoToSave)
        {
            Instrumento instrumentoExiste = await GetInstrumentoByNameAsync(context, instrumentoToSave.Nombre);
            if (instrumentoExiste != null)
            {
                Console.WriteLine("El instrumento " + instrumentoExiste + " ya existe en la DB!");
            }
            else
            {
                context.Instrumentos.Add(instrumentoToSave);
            }
            return await context.SaveChangesAsync();
        }

        public static async Task<int> DeleteInstrumento(OrquestaOESATContext context, Instrumento instrumentoToDelete)
        {
            Instrumento instrumentoExiste = await GetInstrumentoByNameAsync(context, instrumentoToDelete.Nombre);
            if (instrumentoExiste != null)
            {
                context.Remove(instrumentoToDelete);
            }
            else
            {
                instrumentoExiste = await GetInstrumentoById(context, instrumentoToDelete.InstrumentoId);
                if (instrumentoExiste != null)
                {
                    context.Remove(instrumentoToDelete);
                }
                else
                {
                    Console.WriteLine("El instrumento no existe");
                }
            }
            return await context.SaveChangesAsync();
        }

        public static async Task ModificarInstrumentoAsync(OrquestaOESATContext context, int instrumentoId, Instrumento instrumentoModificado)
        {
            var instrumentoExistente = await context.Instrumentos.FindAsync(instrumentoId);

            if (instrumentoExistente != null)
            {
                // Actualizar propiedades según tu lógica de modificación
                instrumentoExistente.Nombre = instrumentoModificado.Nombre ?? instrumentoExistente.Nombre;
                instrumentoExistente.Detalles = instrumentoModificado.Detalles ?? instrumentoExistente.Detalles;

                await context.SaveChangesAsync();
            }
        }

        private static async Task<Instrumento?> GetInstrumentoById(OrquestaOESATContext context, int instrumentoId)
        {
            return await context.Instrumentos.FirstOrDefaultAsync(p => p.InstrumentoId == instrumentoId);
        }

        public static async Task<Instrumento> GetInstrumentoByNameAsync(OrquestaOESATContext context, string nombreInstrumento)
        {
            return await context.Instrumentos.FirstOrDefaultAsync(p => p.Nombre == nombreInstrumento);
        }

        public static List<Instrumento> GetInstrumentos(OrquestaOESATContext context)
        {
            var instrumentos = context.Instrumentos.ToList();
            return instrumentos;
        }

        public static async Task PrestarInstrumentoAsync(OrquestaOESATContext context, Guid estudianteId, int instrumentoId)
        {
            var estudiante = await context.Estudiantes.FindAsync(estudianteId);
            var instrumento = await context.Instrumentos.FindAsync(instrumentoId);

            if (estudiante != null && instrumento != null && instrumento.Disponible)
            {
                // Crear un nuevo préstamo
                var prestamo = new PrestamoInstrumento
                {
                    FechaPrestamo = DateTime.Now,
                    Instrumento = instrumento,
                    Estudiante = estudiante
                };

                // Actualizar el estado del instrumento a no disponible
                instrumento.Disponible = false;

                // Agregar el préstamo a la base de datos
                context.PrestamosInstrumentos.Add(prestamo);

                await context.SaveChangesAsync();
            }
        }

        public static async Task DevolverInstrumentoAsync(OrquestaOESATContext context, Guid estudianteId, int instrumentoId)
        {
            var prestamo = await context.PrestamosInstrumentos
                .Where(p => p.EstudianteId == estudianteId && p.InstrumentoId == instrumentoId && p.FechaDevolucion == null)
                .FirstOrDefaultAsync();

            if (prestamo != null)
            {
                // Actualizar la fecha de devolución
                prestamo.FechaDevolucion = DateTime.Now;

                // Actualizar el estado del instrumento a disponible
                prestamo.Instrumento.Disponible = true;

                // Guardar los cambios en la base de datos
                await context.SaveChangesAsync();
            }
        }



        #endregion


        #region Curso

        public static async Task<int> SaveCurso(OrquestaOESATContext context, Curso cursoToSave)
        {
            Curso cursoExiste = await GetCursoByNameAsync(context, cursoToSave.Nombre);
            if (cursoExiste != null)
            {
                Console.WriteLine("El curso " + cursoExiste + " ya existe en la DB!");
            }
            else
            {
                context.Cursos.Add(cursoToSave);
            }
            return await context.SaveChangesAsync();
        }

        public static async Task<int> DeleteCurso(OrquestaOESATContext context, Curso cursoToDelete)
        {
            Curso cursoExiste = await GetCursoByNameAsync(context, cursoToDelete.Nombre);
            if (cursoExiste != null)
            {
                context.Remove(cursoToDelete);
            }
            else
            {
                cursoExiste = await GetCursoById(context, cursoToDelete.CursoId);
                if (cursoExiste != null)
                {
                    context.Remove(cursoToDelete);
                }
                else
                {
                    Console.WriteLine("El curso no existe");
                }
            }
            return await context.SaveChangesAsync();
        }

        public static async Task ModificarCursoAsync(OrquestaOESATContext context, int cursoId, Curso cursoModificado)
        {
            var cursoExistente = await context.Cursos.FindAsync(cursoId);

            if (cursoExistente != null)
            {
                // Actualizar propiedades según tu lógica de modificación
                cursoExistente.Nombre = cursoModificado.Nombre ?? cursoExistente.Nombre;
                cursoExistente.ProfesorId = cursoModificado.ProfesorId ?? cursoExistente.ProfesorId;

                await context.SaveChangesAsync();
            }
        }

        private static async Task<Curso?> GetCursoById(OrquestaOESATContext context, int cursoId)
        {
            return await context.Cursos.FirstOrDefaultAsync(p => p.CursoId == cursoId);
        }

        public static async Task<Curso> GetCursoByNameAsync(OrquestaOESATContext context, string nombreCurso)
        {
            return await context.Cursos.FirstOrDefaultAsync(p => p.Nombre == nombreCurso);
        }

        public static List<Curso> GetCursos(OrquestaOESATContext context)
        {
            var cursos = context.Cursos.ToList();
            return cursos;
        }


     

        #endregion
    }
}





