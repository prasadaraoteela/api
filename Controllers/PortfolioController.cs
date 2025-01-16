using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using api.Extensions;
using api.Interfaces;
using api.Models;

namespace api.Controllers
{
    [ApiController]
    [Route("api/portfolios")]
    public class PortfolioController(UserManager<StockUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var userName = User.GetUserName();
            var stockUser = await userManager.FindByNameAsync(userName);
            var userPortfolio = await portfolioRepository.GetUserPortfolio(stockUser!);

            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var userName = User.GetUserName();
            var stockUser = await userManager.FindByNameAsync(userName);
            var stock = await stockRepository.GetBySymbolAsync(symbol);

            if (stockUser == null) return Unauthorized("User not found.");

            if (stock == null) return BadRequest("Stock not found.");

            var userPortfolio = await portfolioRepository.GetUserPortfolio(stockUser);

            if (userPortfolio.Any(stock => stock.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio.");

            var portfolio = new Portfolio
            {
                StockId = stock.Id,
                StockUserId = stockUser.Id
            };
            portfolio = await portfolioRepository.CreatePortfolioAsync(portfolio);

            if (portfolio == null)
            {
                return StatusCode(500, "Could not create portfolio.");
            }
            else
            {
                return Created();
            }
        }
    }
}