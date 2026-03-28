using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaGestionOrquesta.Models;
using SistemaGestionOrquesta.DTOs;
using System;

namespace SistemaGestionOrquesta.Services
{
    public class StockService
    {
        private readonly OrquestaOESATContext _context;

        public StockService(OrquestaOESATContext context)
        {
            _context = context;
        }

        public async Task<StockInstrumento> CreateStockAsync(CreateStockDto dto)
        {
            var instrumento = await _context.Instrumentos.FindAsync(dto.InstrumentoId);
            if (instrumento is null)
                throw new KeyNotFoundException($"Instrumento {dto.InstrumentoId} no existe.");

            var exists = await _context.StockInstrumentos
                .AnyAsync(s => s.CodigoInventario == dto.CodigoInventario);
            if (exists)
                throw new InvalidOperationException($"CodigoInventario '{dto.CodigoInventario}' ya existe.");

            var stock = new StockInstrumento
            {
                InstrumentoId = dto.InstrumentoId,
                CodigoInventario = dto.CodigoInventario,
                NumeroSerie = dto.NumeroSerie,
                Estado = "Disponible"
            };

            _context.StockInstrumentos.Add(stock);
            await _context.SaveChangesAsync();

            // Mantener campo Instrumento.Disponible coherente (opcional pero práctico)
            instrumento.Disponible = await _context.StockInstrumentos
                .AnyAsync(s => s.InstrumentoId == instrumento.InstrumentoId && s.Estado == "Disponible");

            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<List<StockInstrumento>> GetDisponiblesAsync(int instrumentoId)
        {
            return await _context.StockInstrumentos
                .Where(s => s.InstrumentoId == instrumentoId && s.Estado == "Disponible")
                .ToListAsync();
        }

        public async Task<StockInstrumento?> GetByIdAsync(int id)
        {
            return await _context.StockInstrumentos.FindAsync(id);
        }
    }
}