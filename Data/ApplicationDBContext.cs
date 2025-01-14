using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using api.Models;

namespace api.Data
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : IdentityDbContext<StockUser>(options)
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(entityBuilder => entityBuilder.HasKey(portfolio => new { portfolio.StockUserId, portfolio.StockId }));

            builder.Entity<Portfolio>()
                .HasOne(portfolio => portfolio.StockUser)
                .WithMany(stockUser => stockUser.Portfolios)
                .HasForeignKey(portfolio => portfolio.StockUserId);

            builder.Entity<Portfolio>()
                .HasOne(portfolio => portfolio.Stock)
                .WithMany(stockUser => stockUser.Portfolios)
                .HasForeignKey(portfolio => portfolio.StockId);

            // List<IdentityRole> roles =
            // [
            //     new IdentityRole
            //     {
            //         Name = "Admin",
            //         NormalizedName ="ADMIN"
            //     },
            //     new IdentityRole
            //     {
            //         Name = "User",
            //         NormalizedName ="USER"
            //     }
            // ];
            // builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}