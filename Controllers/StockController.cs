using Microsoft.AspNetCore.Mvc;

using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;

namespace api.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController(IStockRepository stockRepository) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<StockDto>>> GetStocks()
        {
            var stocks = await stockRepository.GetAllAsync();
            return Ok(stocks.Select(stock => stock.ToStockDto()).ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockDto>> GetStockById([FromRoute] int id)
        {
            var stock = await stockRepository.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<ActionResult<StockDto>> CreateStock([FromBody] CreateStockDto createStockDto)
        {
            var stock = await stockRepository.CreateAsync(createStockDto.ToStock());

            return CreatedAtAction(nameof(GetStockById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<StockDto>> UpdateStock([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
        {
            var stock = await stockRepository.UpdateAsync(id, updateStockDto);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = await stockRepository.DeleteAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}