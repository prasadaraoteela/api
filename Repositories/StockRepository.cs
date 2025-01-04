using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;

namespace api.Repositories
{
    public class StockRepository(ApplicationDBContext context) : IStockRepository
    {
        public async Task<Stock> CreateAsync(Stock stock)
        {
            await context.Stocks.AddAsync(stock);
            await context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return null;
            }

            context.Stocks.Remove(stock);
            await context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await context.Stocks.Include(stock => stock.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await context.Stocks.Include(stock => stock.Comments).FirstOrDefaultAsync(stock => stock.Id == id);

            if (stock == null)
            {
                return null;
            }
            return stock;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto updateStockDto)
        {
            var stock = await context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return null;
            }

            stock.Symbol = updateStockDto.Symbol;
            stock.CompanyName = updateStockDto.CompanyName;
            stock.Price = updateStockDto.Price;
            stock.Dividend = updateStockDto.Dividend;
            stock.Industry = updateStockDto.Industry;
            stock.MarketCap = updateStockDto.MarketCap;

            await context.SaveChangesAsync();
            return stock;
        }
    }
}