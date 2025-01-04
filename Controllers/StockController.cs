using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Dtos.Stock;
using api.Mappers;

namespace api.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController(ApplicationDBContext context) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<StockDto>>> GetStocks()
        {
            var stocks = await context.Stocks.ToListAsync();
            return stocks.Select(stock => stock.ToStockDto()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockDto>> GetStockById([FromRoute] int id)
        {
            var stock = await context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return stock.ToStockDto();
        }

        [HttpPost]
        public async Task<ActionResult<StockDto>> CreateStock([FromBody] CreateStockDto createStockDto)
        {
            var stock = createStockDto.ToStock();
            context.Stocks.Add(stock);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStockById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<StockDto>> UpdateStock([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
        {
            var stock = await context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            stock.Symbol = updateStockDto.Symbol;
            stock.CompanyName = updateStockDto.CompanyName;
            stock.Price = updateStockDto.Price;
            stock.Dividend = updateStockDto.Dividend;
            stock.Industry = updateStockDto.Industry;
            stock.MarketCap = updateStockDto.MarketCap;

            await context.SaveChangesAsync();

            return stock.ToStockDto();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = await context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            context.Stocks.Remove(stock);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}