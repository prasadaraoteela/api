using api.Models;

namespace api.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(StockUser user);
        Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio);
    }
}