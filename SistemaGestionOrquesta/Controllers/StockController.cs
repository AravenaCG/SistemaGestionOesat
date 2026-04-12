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
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _stockService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("disponibles/{instrumentoId}")]
        public async Task<IActionResult> GetDisponibles(int instrumentoId)
        {
            var list = await _stockService.GetDisponiblesAsync(instrumentoId);
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stock = await _stockService.GetByIdAsync(id);
            if (stock is null) return NotFound();
            return Ok(stock);
        }

        [HttpGet("codigo/{codigoInventario}")]
        public async Task<IActionResult> GetByCodigo(string codigoInventario)
        {
            var stock = await _stockService.GetByCodigoInventarioAsync(codigoInventario);
            if (stock is null) return NotFound();
            return Ok(stock);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStockDto dto)
        {
            try
            {
                var updated = await _stockService.UpdateStockAsync(id, dto);
                return Ok(updated);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _stockService.DeleteStockAsync(id);
                return NoContent();
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
    }
}