using System;

namespace SistemaGestionOrquesta.DTOs
{
    public record CreateStockDto(int InstrumentoId, string CodigoInventario, string? NumeroSerie);

    public record AsignarPrestamoDto(Guid EstudianteId, int StockInstrumentoId);

    public record DevolverPrestamoDto(int StockInstrumentoId);
}