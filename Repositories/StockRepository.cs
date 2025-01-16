using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Dtos.Stock;
using api.Helpers;
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

        public async Task<List<Stock>> GetAllAsync(StockQuery query)
        {
            var stocks = context.Stocks.Include(stock => stock.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(stock => stock.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(stock => stock.Symbol.Contains(query.Symbol));
            }

            if (query.SortBy != null)
            {
                switch (query.SortBy)
                {
                    case SortByOptions.Symbol:
                        stocks = query.IsDecsending ? stocks.OrderByDescending(stock => stock.Symbol) : stocks.OrderBy(stock => stock.Symbol);
                        break;
                    case SortByOptions.CompanyName:
                        stocks = query.IsDecsending ? stocks.OrderByDescending(stock => stock.CompanyName) : stocks.OrderBy(stock => stock.CompanyName);
                        break;
                    case SortByOptions.Price:
                        stocks = query.IsDecsending ? stocks.OrderByDescending(stock => stock.Price) : stocks.OrderBy(stock => stock.Price);
                        break;
                    default: break;
                }
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
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

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await context.Stocks.FirstOrDefaultAsync(stock => stock.Symbol == symbol);
        }

        public async Task<bool> StockExists(int id)
        {
            return await context.Stocks.AnyAsync(stock => stock.Id == id);
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