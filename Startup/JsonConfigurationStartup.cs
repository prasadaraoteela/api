using System.Text.Json;

namespace api.Startup
{
    public class JsonConfigurationStartup : IStartup
    {
        public void Configure(WebApplication application)
        {
        }

        public void ConfigureBuilder(WebApplicationBuilder builder)
        {
            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
        }
    }
}