using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Interfaces;
using api.Models;
using api.Mappers;

namespace api.Repositories
{
    public class PortfolioRepository(ApplicationDBContext context) : IPortfolioRepository
    {
        public async Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio)
        {
            await context.Portfolios.AddAsync(portfolio);
            await context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(StockUser user)
        {
            return await context.Portfolios
                .Where(portfolio => portfolio.StockUserId == user.Id)
                .Select(portfolio => portfolio.ToStock())
                .ToListAsync();
        }
    }
}