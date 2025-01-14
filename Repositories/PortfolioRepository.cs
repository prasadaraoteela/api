using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Interfaces;
using api.Models;

namespace api.Repositories
{
    public class PortfolioRepository(ApplicationDBContext context) : IPortfolioRepository
    {
        public Task<List<Stock>> GetUserPortfolio(StockUser user)
        {
            return context.Portfolios.Where(portfolio => portfolio.StockUserId == user.Id)
            .Select(stock => new Stock
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Price = stock.Stock.Price,
                Dividend = stock.Stock.Dividend,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap
            }).ToListAsync();
        }
    }
}