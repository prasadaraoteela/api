using api.Interfaces;
using api.Repositories;
using api.Services;

namespace api.Startup
{
    public class DependencyInjection : IStartup
    {
        public void Configure(WebApplication application)
        {
        }

        public void ConfigureBuilder(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IStockRepository, StockRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        }
    }
}