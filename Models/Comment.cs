using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnCsharp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        public Stock? Stock { get; set; }
    }
}