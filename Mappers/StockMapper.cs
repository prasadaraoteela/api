using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stock)
        {
            return new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Price = stock.Price,
                Dividend = stock.Dividend,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Comments = stock.Comments.Select(comment => comment.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStock(this CreateStockDto createStockDto)
        {
            return new Stock
            {
                Symbol = createStockDto.Symbol,
                CompanyName = createStockDto.CompanyName,
                Price = createStockDto.Price,
                Dividend = createStockDto.Dividend,
                Industry = createStockDto.Industry,
                MarketCap = createStockDto.MarketCap
            };
        }
    }
}