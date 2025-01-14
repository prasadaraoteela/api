using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    [Table("StockUsers")]
    public class StockUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = [];
    }
}