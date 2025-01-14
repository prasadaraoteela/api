using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using api.Extensions;
using api.Interfaces;
using api.Models;

namespace api.Controllers
{
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
    }
}