namespace api.Dtos.Comment
{
    public class UpdateCommentDto
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
    }
}