using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaGestionOrquesta.Models;
using SistemaGestionOrquesta.DTOs;
using System.Linq;

namespace SistemaGestionOrquesta.Services
{
    public class PrestamosService
    {
        private readonly OrquestaOESATContext _context;

        public PrestamosService(OrquestaOESATContext context)
        {
            _context = context;
        }

        public async Task<PrestamosInstrumento> AsignarPrestamoAsync(AsignarPrestamoDto dto)
        {
            await using var tx = await _context.Database.BeginTransactionAsync();

            var stock = await _context.StockInstrumentos
                .Include(s => s.Instrumento)
                .FirstOrDefaultAsync(s => s.StockInstrumentoId == dto.StockInstrumentoId);
            if (stock is null)
                throw new KeyNotFoundException($"StockInstrumento {dto.StockInstrumentoId} no existe.");

            if (stock.Estado != "Disponible")
                throw new InvalidOperationException("El ejemplar no está disponible para préstamo.");

            var estudiante = await _context.Estudiantes.FindAsync(dto.EstudianteId);
            if (estudiante is null)
                throw new KeyNotFoundException($"Estudiante {dto.EstudianteId} no existe.");

            var prestamo = new PrestamosInstrumento
            {
                FechaPrestamo = DateTime.UtcNow,
                FechaDevolucion = null,
                InstrumentoId = stock.InstrumentoId,
                StockInstrumentoId = stock.StockInstrumentoId,
                EstudianteId = dto.EstudianteId
            };

            _context.PrestamosInstrumentos.Add(prestamo);

            stock.Estado = "Prestado";
            _context.StockInstrumentos.Update(stock);

            await _context.SaveChangesAsync();

            // Actualizar Instrumento.Disponible según queden ejemplares disponibles
            var quedanDisponibles = await _context.StockInstrumentos
                .AnyAsync(s => s.InstrumentoId == stock.InstrumentoId && s.Estado == "Disponible");

            stock.Instrumento.Disponible = quedanDisponibles;
            _context.Instrumentos.Update(stock.Instrumento);
            await _context.SaveChangesAsync();

            await tx.CommitAsync();

            return prestamo;
        }

        public async Task<PrestamosInstrumento> DevolverPrestamoAsync(DevolverPrestamoDto dto)
        {
            await using var tx = await _context.Database.BeginTransactionAsync();

            var stock = await _context.StockInstrumentos
                .Include(s => s.Instrumento)
                .FirstOrDefaultAsync(s => s.StockInstrumentoId == dto.StockInstrumentoId);
            if (stock is null)
                throw new KeyNotFoundException($"StockInstrumento {dto.StockInstrumentoId} no existe.");

            var prestamo = await _context.PrestamosInstrumentos
                .Where(p => p.StockInstrumentoId == dto.StockInstrumentoId && p.FechaDevolucion == null)
                .OrderByDescending(p => p.FechaPrestamo)
                .FirstOrDefaultAsync();

            if (prestamo is null)
                throw new InvalidOperationException("No existe un préstamo abierto para ese ejemplar.");

            prestamo.FechaDevolucion = DateTime.UtcNow;
            _context.PrestamosInstrumentos.Update(prestamo);

            stock.Estado = "Disponible";
            _context.StockInstrumentos.Update(stock);

            // Actualizar Instrumento.Disponible: si hay al menos uno disponible -> true
            var hayDisponibles = await _context.StockInstrumentos
                .AnyAsync(s => s.InstrumentoId == stock.InstrumentoId && s.Estado == "Disponible");

            stock.Instrumento.Disponible = hayDisponibles;
            _context.Instrumentos.Update(stock.Instrumento);

            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            return prestamo;
        }
    }
}