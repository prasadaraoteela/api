using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        required public string StockUserId { get; set; } = string.Empty;
        required public int StockId { get; set; }
        required public StockUser StockUser { get; set; }
        required public Stock Stock { get; set; }
    }
}