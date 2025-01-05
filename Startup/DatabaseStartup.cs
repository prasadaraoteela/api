using Microsoft.EntityFrameworkCore;

using api.Data;

namespace api.Startup
{
    public class DatabaseStartup : IStartup
    {
        public void Configure(WebApplication application)
        {
        }

        public void ConfigureBuilder(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}