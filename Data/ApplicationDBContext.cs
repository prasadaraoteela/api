using Microsoft.EntityFrameworkCore;

using api.Models;

namespace api.Data
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
    {
        public required DbSet<Stock> Stocks { get; set; }
        public required DbSet<Comment> Comments { get; set; }
    }
}