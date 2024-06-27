using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SistemaGestionOrquesta.Models;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace SistemaGestionOrquesta.Utils
{
    internal class LinQueris
    {
        #region ABM_Estudiante

        public static async Task<int> SaveEstudiante(OrquestaOESATContext context, Estudiante estudianteToSave)
        {
            try
            {
                context.Estudiantes.Add(estudianteToSave);
                return await context.SaveChangesAsync();
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

        public static async Task<Estudiante> BuscarEstudiantePorNombreYDniAsync(OrquestaOESATContext context, string nombre, string documento)
        {
            return await context.Estudiantes.FirstOrDefaultAsync(p => p.Nombre == nombre && p.Documento == documento);
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

        public static async Task<bool> InscribirEstudianteEnCurso(OrquestaOESATContext context, Guid estudianteId, int cursoId)
        {
            try
            {
                // Obtener el estudiante por su ID
                var estudiante = await context.Estudiantes
                    .Include(e => e.Cursos)  // Asegurarse de incluir la relación de cursos
                    .FirstOrDefaultAsync(e => e.EstudianteId == estudianteId);

                if (estudiante != null)
                {
                    // Obtener el curso por su ID
                    var curso = await context.Cursos.FindAsync(cursoId);

                    if (curso != null)
                    {
                        // Verificar si el estudiante ya está inscrito en el curso
                        if (!estudiante.Cursos.Any(c => c.CursoId == cursoId))
                        {
                            // Agregar el curso al estudiante
                            estudiante.Cursos.Add(curso);

                            // Guardar los cambios en la base de datos
                            await context.SaveChangesAsync();

                            return true; // Éxito al inscribir al estudiante en el curso
                        }
                        else
                        {
                            // El estudiante ya está inscrito en el curso
                            return false;
                        }
                    }
                    else
                    {
                        // No se encontró el curso con el ID proporcionado
                        return false;
                    }
                }
                else
                {
                    // No se encontró el estudiante con el ID proporcionado
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                Console.WriteLine($"Error al inscribir estudiante en curso: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> DarDeBajaEstudianteDeCurso(OrquestaOESATContext context, Guid estudianteId, int cursoId)
        {
            try
            {
                // Obtener el estudiante por su ID
                var estudiante = await context.Estudiantes
                    .Include(e => e.Cursos)  // Asegurarse de incluir la relación de cursos
                    .FirstOrDefaultAsync(e => e.EstudianteId == estudianteId);

                if (estudiante != null)
                {
                    // Obtener el curso por su ID
                    var curso = await context.Cursos.FindAsync(cursoId);

                    if (curso != null)
                    {
                        // Verificar si el estudiante está inscrito en el curso
                        var inscripcion = estudiante.Cursos.FirstOrDefault(c => c.CursoId == cursoId);

                        if (inscripcion != null)
                        {
                            // Quitar el curso de la lista de cursos del estudiante
                            estudiante.Cursos.Remove(inscripcion);

                            // Guardar los cambios en la base de datos
                            await context.SaveChangesAsync();

                            return true; // Éxito al dar de baja al estudiante del curso
                        }
                        else
                        {
                            // El estudiante no está inscrito en el curso
                            return false;
                        }
                    }
                    else
                    {
                        // No se encontró el curso con el ID proporcionado
                        return false;
                    }
                }
                else
                {
                    // No se encontró el estudiante con el ID proporcionado
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                Console.WriteLine($"Error al dar de baja estudiante del curso: {ex.Message}");
                return false;
            }
        }
        public static async Task CambiarEstudianteDeCursoAsync(OrquestaOESATContext context, Guid estudianteId, int nuevoCursoId, int viejoCursoId)
        {
            // Ejecutar ambas tareas de forma paralela y esperar a que ambas se completen
            var tareas = new List<Task<bool>>
    {
        LinQueris.DarDeBajaEstudianteDeCurso(context, estudianteId, viejoCursoId),
        LinQueris.InscribirEstudianteEnCurso(context, estudianteId, nuevoCursoId)
    };

            await Task.WhenAll(tareas);

            // Puedes manejar el resultado de las tareas si es necesario
            bool bajaExitosa = tareas[0].Result;
            bool altaExitosa = tareas[1].Result;

            // Realizar cualquier otro manejo necesario después de la actualización
            if (bajaExitosa && altaExitosa)
            {
                Console.WriteLine($"Estudiante {estudianteId} cambió de {viejoCursoId} a {nuevoCursoId}");
            }
            else
            {
                Console.WriteLine($"Error al cambiar estudiante de curso");
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

        public static async Task<string> DeleteProfesor(OrquestaOESATContext context, Profesor profesorToDelete)
        {
            try
            {
                context.Remove(profesorToDelete);
                await context.SaveChangesAsync();
                return "Profesor eliminado exitosamente!";
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

        public static async Task DarBajaProfesorAsync(OrquestaOESATContext context, Guid profesorId)
        {
            var profesor = await context.Profesors.FindAsync(profesorId);

            if (profesor != null)
            {
                profesor.Activo = false; // Establecer el campo Activo como false para dar la baja lógica
                await context.SaveChangesAsync(); // Guardar los cambios en la base de datos
            }
        }

        public static async Task<string> ModificarProfesorAsync(OrquestaOESATContext context, Guid profesorId, Profesor profesorModificado)
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
                return "Estudiante Modificado Exitosamente!";
            }
            else { return "El estudiante al parecer no existe en la DB"; }
        
        }

        public static async Task<Profesor> GetProfesorById(OrquestaOESATContext context, Guid profesorId)
        {
            return await context.Profesors.FirstOrDefaultAsync(p => p.ProfesorId == profesorId);
        }

        public static async Task<Profesor> GetProfesorByNameAsync(OrquestaOESATContext context, string nombreProfesor, string apellidoProfesor)
        {
            return await context.Profesors.FirstOrDefaultAsync(p => p.Nombre == nombreProfesor && p.Apellido == apellidoProfesor);
        }

        public static async Task<Profesor> GetProfesorPorNombreYDniAsync(OrquestaOESATContext context, string nombreProfesor, string apellidoProfesor)
        {
            return await context.Profesors.FirstOrDefaultAsync(p => p.Nombre == nombreProfesor && p.Documento == apellidoProfesor);
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

        public static async Task<string> DeleteInstrumento(OrquestaOESATContext context, Instrumento instrumentoToDelete)
        {
            try
            {
                // Intenta eliminar el estudiante
                context.Remove(instrumentoToDelete);
                await context.SaveChangesAsync();
                return "Instrumento eliminado exitosamente!";
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

        public static async Task<string> ModificarInstrumentoAsync(OrquestaOESATContext context, int instrumentoId, Instrumento instrumentoModificado)
        {
            var instrumentoExistente = await context.Instrumentos.FindAsync(instrumentoId);

            if (instrumentoExistente != null)
            {
                // Actualizar propiedades según tu lógica de modificación
                instrumentoExistente.Nombre = instrumentoModificado.Nombre ?? instrumentoExistente.Nombre;
                instrumentoExistente.Detalles = instrumentoModificado.Detalles ?? instrumentoExistente.Detalles;

                return "Instrumento Modificado Exitosamente!";
            }
            else { return "El instrumento al parecer no existe en la DB"; }
        }

        public static async Task<Instrumento?> GetInstrumentoById(OrquestaOESATContext context, int instrumentoId)
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
                PrestamosInstrumento prestamo = new PrestamosInstrumento
                {
                    FechaPrestamo = System.DateTime.Now,
                    Instrumento = instrumento,
                    Estudiante = estudiante,
                    EstudianteId = estudianteId,
                    InstrumentoId = instrumentoId
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
                prestamo.FechaDevolucion = System.DateTime.Now;

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

        public static async Task<string> DeleteCurso(OrquestaOESATContext context, Curso cursoToDelete)
        {
            try
            {
                // Intenta eliminar el estudiante
                context.Remove(cursoToDelete);
                await context.SaveChangesAsync();
                return "Curso eliminado exitosamente!";
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

        public static async Task<string> ModificarCursoAsync(OrquestaOESATContext context, int cursoId, Curso cursoModificado)
        {
            var cursoExistente = await context.Cursos.FindAsync(cursoId);

            if (cursoExistente != null)
            {
                cursoExistente.Nombre = cursoModificado.Nombre ?? cursoExistente.Nombre;
                cursoExistente.ProfesorId = cursoModificado.ProfesorId ?? cursoExistente.ProfesorId;

                await context.SaveChangesAsync();
                return "Curso Modificado Exitosamente!";
            }
            else { return "El curso al parecer no existe en la DB"; }
        }
        

        public static async Task<Curso?> GetCursoById(OrquestaOESATContext context, int cursoId)
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





