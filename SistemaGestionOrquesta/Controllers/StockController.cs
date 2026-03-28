using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SistemaGestionOrquesta.Services;
using SistemaGestionOrquesta.DTOs;
using SistemaGestionOrquesta.Models;
using System;

namespace SistemaGestionOrquesta.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly StockService _stockService;

        public StockController(StockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto dto)
        {
            try
            {
                var created = await _stockService.CreateStockAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.StockInstrumentoId }, created);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
            catch (InvalidOperationException ioe)
            {
                return Conflict(ioe.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("disponibles/{instrumentoId}")]
        public async Task<IActionResult> GetDisponibles(int instrumentoId)
        {
            var list = await _stockService.GetDisponiblesAsync(instrumentoId);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stock = await _stockService.GetByIdAsync(id);
            if (stock is null) return NotFound();
            return Ok(stock);
        }
    }
}