using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        [ForeignKey("Stock")]
        public int? StockId { get; set; }
        public Stock? Stock { get; set; }
        public string StockUserId { get; set; }
        public StockUser StockUser { get; set; }
    }
}