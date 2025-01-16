using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class StockUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = [];
    }
}