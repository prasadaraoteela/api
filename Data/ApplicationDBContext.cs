using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Data
{
    public class ApplicationDBContext(DbContextOptions dbContextOptions) : IdentityDbContext<StockUser>(dbContextOptions)
    {
        public required DbSet<Stock> Stocks { get; set; }
        public required DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<IdentityRole> roles =
            [
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName ="ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName ="USER"
                }
            ];
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}