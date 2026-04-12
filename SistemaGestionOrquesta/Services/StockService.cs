using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaGestionOrquesta.Models;
using SistemaGestionOrquesta.DTOs;
using System;

namespace SistemaGestionOrquesta.Services
{
    public class StockService : IStockService
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

            instrumento.Disponible = await _context.StockInstrumentos
                .AnyAsync(s => s.InstrumentoId == instrumento.InstrumentoId && s.Estado == "Disponible");

            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<List<StockInstrumento>> GetAllAsync()
        {
            return await _context.StockInstrumentos
                .Include(s => s.Instrumento)
                .ToListAsync();
        }

        public async Task<List<StockInstrumento>> GetDisponiblesAsync(int instrumentoId)
        {
            return await _context.StockInstrumentos
                .Where(s => s.InstrumentoId == instrumentoId && s.Estado == "Disponible")
                .ToListAsync();
        }

        public async Task<StockInstrumento?> GetByIdAsync(int id)
        {
            return await _context.StockInstrumentos
                .Include(s => s.Instrumento)
                .FirstOrDefaultAsync(s => s.StockInstrumentoId == id);
        }

        public async Task<StockInstrumento?> GetByCodigoInventarioAsync(string codigoInventario)
        {
            return await _context.StockInstrumentos
                .Include(s => s.Instrumento)
                .FirstOrDefaultAsync(s => s.CodigoInventario == codigoInventario);
        }

        public async Task<StockInstrumento> UpdateStockAsync(int id, UpdateStockDto dto)
        {
            var stock = await _context.StockInstrumentos
                .Include(s => s.Instrumento)
                .FirstOrDefaultAsync(s => s.StockInstrumentoId == id);
            if (stock is null)
                throw new KeyNotFoundException($"StockInstrumento {id} no existe.");

            // Solo actualizar los campos que vengan con valor
            if (!string.IsNullOrWhiteSpace(dto.CodigoInventario) && dto.CodigoInventario != stock.CodigoInventario)
            {
                var duplicado = await _context.StockInstrumentos
                    .AnyAsync(s => s.CodigoInventario == dto.CodigoInventario && s.StockInstrumentoId != id);
                if (duplicado)
                    throw new InvalidOperationException($"CodigoInventario '{dto.CodigoInventario}' ya existe.");
                stock.CodigoInventario = dto.CodigoInventario;
            }

            if (dto.NumeroSerie is not null)
                stock.NumeroSerie = dto.NumeroSerie;

            if (!string.IsNullOrWhiteSpace(dto.Estado))
                stock.Estado = dto.Estado;

            _context.StockInstrumentos.Update(stock);
            await _context.SaveChangesAsync();

            // Mantener Instrumento.Disponible coherente
            stock.Instrumento.Disponible = await _context.StockInstrumentos
                .AnyAsync(s => s.InstrumentoId == stock.InstrumentoId && s.Estado == "Disponible");
            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task DeleteStockAsync(int id)
        {
            var stock = await _context.StockInstrumentos.FindAsync(id);
            if (stock is null)
                throw new KeyNotFoundException($"StockInstrumento {id} no existe.");

            var tienePrestamosAbiertos = await _context.PrestamosInstrumentos
                .AnyAsync(p => p.StockInstrumentoId == id && p.FechaDevolucion == null);
            if (tienePrestamosAbiertos)
                throw new InvalidOperationException("No se puede eliminar: tiene préstamos abiertos.");

            _context.StockInstrumentos.Remove(stock);
            await _context.SaveChangesAsync();

            // Actualizar Instrumento.Disponible
            var instrumento = await _context.Instrumentos.FindAsync(stock.InstrumentoId);
            if (instrumento is not null)
            {
                instrumento.Disponible = await _context.StockInstrumentos
                    .AnyAsync(s => s.InstrumentoId == stock.InstrumentoId && s.Estado == "Disponible");
                await _context.SaveChangesAsync();
            }
        }
    }

    public interface IStockService
    {
        Task<StockInstrumento> CreateStockAsync(CreateStockDto dto);
        Task<List<StockInstrumento>> GetAllAsync();
        Task<List<StockInstrumento>> GetDisponiblesAsync(int instrumentoId);
        Task<StockInstrumento?> GetByIdAsync(int id);
        Task<StockInstrumento?> GetByCodigoInventarioAsync(string codigoInventario);
        Task<StockInstrumento> UpdateStockAsync(int id, UpdateStockDto dto);
        Task DeleteStockAsync(int id);
    }
}