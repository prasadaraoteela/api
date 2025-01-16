using api.Models;

namespace api.Mappers
{
    public static class PortfolioMapper
    {
        public static Stock ToStock(this Portfolio portfolio)
        {
            if (portfolio.Stock == null)
            {
                throw new ArgumentNullException(nameof(portfolio.Stock), "Stock property cannot be null");
            }

            return new Stock
            {
                Id = portfolio.StockId,
                Symbol = portfolio.Stock.Symbol,
                CompanyName = portfolio.Stock.CompanyName,
                Price = portfolio.Stock.Price,
                Dividend = portfolio.Stock.Dividend,
                Industry = portfolio.Stock.Industry,
                MarketCap = portfolio.Stock.MarketCap
            };
        }
    }
}