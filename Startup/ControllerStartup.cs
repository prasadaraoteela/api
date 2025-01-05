using Newtonsoft.Json.Converters;

namespace api.Startup
{
    public class ControllerStartup : IStartup
    {
        public void Configure(WebApplication application)
        {
            application.UseRouting();
            application.MapControllers();
            application.UseHttpsRedirection();
        }

        public void ConfigureBuilder(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
        }
    }
}