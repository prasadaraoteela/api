using Microsoft.AspNetCore.Mvc;

using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController(IStockRepository stockRepository) : ControllerBase
    {

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<StockDto>>> GetStocks([FromQuery] StockQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stocks = await stockRepository.GetAllAsync(query);
            return Ok(stocks.Select(stock => stock.ToStockDto()).ToList());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StockDto>> GetStockById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await stockRepository.CreateAsync(createStockDto.ToStock());

            return CreatedAtAction(nameof(GetStockById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<StockDto>> UpdateStock([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await stockRepository.UpdateAsync(id, updateStockDto);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await stockRepository.DeleteAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}