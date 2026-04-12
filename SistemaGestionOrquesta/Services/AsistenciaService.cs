using Microsoft.EntityFrameworkCore;
using SistemaGestionOrquesta.Models;
using SistemaGestionOrquesta.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestionOrquesta.Services
{
    public class AsistenciaService : IAsistenciaService
    {
        private static readonly HashSet<string> EstadosValidos = new(StringComparer.OrdinalIgnoreCase)
        {
            "presente",
            "ausente",
            "ausente_con_aviso"
        };

        private readonly OrquestaOESATContext _context;

        public AsistenciaService(OrquestaOESATContext context)
        {
            _context = context;
        }

        public async Task<List<AsistenciaResponseDto>> GuardarAsistenciaAsync(SaveAttendanceDto dto)
        {
            // Validar que el curso existe
            var curso = await _context.Cursos.FindAsync(dto.CursoId);
            if (curso is null)
                throw new KeyNotFoundException($"Curso {dto.CursoId} no existe.");

            var fechaSolo = dto.Fecha.Date;

            await using var tx = await _context.Database.BeginTransactionAsync();

            // Eliminar registros previos de asistencia para ese curso y fecha (permite re-tomar asistencia)
            var existentes = await _context.Asistencias
                .Where(a => a.CursoId == dto.CursoId && a.Fecha == fechaSolo)
                .ToListAsync();

            if (existentes.Any())
            {
                _context.Asistencias.RemoveRange(existentes);
                await _context.SaveChangesAsync();
            }

            // Insertar los nuevos registros
            var nuevasAsistencias = dto.Asistencias.Select(item =>
            {
                var estado = NormalizarEstadoAsistencia(item.EstadoAsistencia);
                var presente = estado is null ? item.Presente : estado == "presente";

                return new Asistencia
                {
                    EstudianteId = item.EstudianteId,
                    CursoId = dto.CursoId,
                    Fecha = fechaSolo,
                    Presente = presente,
                    EstadoAsistencia = estado ?? (item.Presente ? "presente" : "ausente"),
                    Observacion = item.Observacion
                };
            }).ToList();

            _context.Asistencias.AddRange(nuevasAsistencias);
            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            // Retornar los registros guardados con datos del estudiante y curso
            return await GetByCursoYFechaAsync(dto.CursoId, fechaSolo);
        }

        public async Task<List<AsistenciaResponseDto>> GetByCursoYFechaAsync(int cursoId, DateTime fecha)
        {
            var fechaSolo = fecha.Date;

            return await _context.Asistencias
                .Where(a => a.CursoId == cursoId && a.Fecha == fechaSolo)
                .Include(a => a.Estudiante)
                .Include(a => a.Curso)
                .OrderBy(a => a.Estudiante!.Apellido)
                .ThenBy(a => a.Estudiante!.Nombre)
                .Select(a => new AsistenciaResponseDto
                {
                    AsistenciaId = a.AsistenciaId,
                    EstudianteId = a.EstudianteId,
                    NombreEstudiante = a.Estudiante != null ? a.Estudiante.Nombre : null,
                    ApellidoEstudiante = a.Estudiante != null ? a.Estudiante.Apellido : null,
                    CursoId = a.CursoId,
                    NombreCurso = a.Curso != null ? a.Curso.Nombre : null,
                    Fecha = a.Fecha,
                    Presente = a.Presente,
                    EstadoAsistencia = a.EstadoAsistencia ?? (a.Presente ? "presente" : "ausente"),
                    Observacion = a.Observacion
                })
                .ToListAsync();
        }

        public async Task<List<AsistenciaResponseDto>> GetByEstudianteAsync(Guid estudianteId)
        {
            return await _context.Asistencias
                .Where(a => a.EstudianteId == estudianteId)
                .Include(a => a.Estudiante)
                .Include(a => a.Curso)
                .OrderByDescending(a => a.Fecha)
                .Select(a => new AsistenciaResponseDto
                {
                    AsistenciaId = a.AsistenciaId,
                    EstudianteId = a.EstudianteId,
                    NombreEstudiante = a.Estudiante != null ? a.Estudiante.Nombre : null,
                    ApellidoEstudiante = a.Estudiante != null ? a.Estudiante.Apellido : null,
                    CursoId = a.CursoId,
                    NombreCurso = a.Curso != null ? a.Curso.Nombre : null,
                    Fecha = a.Fecha,
                    Presente = a.Presente,
                    EstadoAsistencia = a.EstadoAsistencia ?? (a.Presente ? "presente" : "ausente"),
                    Observacion = a.Observacion
                })
                .ToListAsync();
        }

        private static string? NormalizarEstadoAsistencia(string? estadoAsistencia)
        {
            if (string.IsNullOrWhiteSpace(estadoAsistencia))
                return null;

            var estadoNormalizado = estadoAsistencia.Trim().ToLowerInvariant();
            if (!EstadosValidos.Contains(estadoNormalizado))
                throw new ArgumentException("EstadoAsistencia inválido. Valores permitidos: presente, ausente, ausente_con_aviso.");

            return estadoNormalizado;
        }
    }
}