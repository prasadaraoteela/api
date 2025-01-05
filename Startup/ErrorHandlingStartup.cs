using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Startup
{
    public class ErrorHandlingStartup : IStartup
    {
        public void Configure(WebApplication application)
        {
            if (!application.Environment.IsDevelopment())
            {
                application.UseExceptionHandler("/error");
            }
            else
            {
                application.UseDeveloperExceptionPage();
            }
            application.MapGet("/error", () => Results.Problem("An error occurred.", statusCode: StatusCodes.Status500InternalServerError));
        }

        public void ConfigureBuilder(WebApplicationBuilder builder)
        {
        }
    }
}