using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string StockUserId { get; set; } = string.Empty;
        public int StockId { get; set; } = 0;
        public StockUser StockUser { get; set; }
        public Stock Stock { get; set; }
    }
}