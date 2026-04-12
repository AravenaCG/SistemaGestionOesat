using System;

namespace SistemaGestionOrquesta.DTOs
{
    public record CreateStockDto(int InstrumentoId, string CodigoInventario, string? NumeroSerie);

    public record UpdateStockDto(string? CodigoInventario, string? NumeroSerie, string? Estado);

    public record AsignarPrestamoDto(Guid EstudianteId, int StockInstrumentoId);

    public record DevolverPrestamoDto(int StockInstrumentoId);
}