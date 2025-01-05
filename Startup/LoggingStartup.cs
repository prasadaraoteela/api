using Microsoft.AspNetCore.HttpLogging;

using Serilog;
using Serilog.Templates;
using Serilog.Templates.Themes;

namespace api.Startup
{
    public class LoggingStartup : IStartup
    {
        public void Configure(WebApplication application)
        {

            application.UseHttpLogging();
            application.UseSerilogRequestLogging();
        }

        public void ConfigureBuilder(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
            });

            builder.Services.AddSerilog((services, config) => config
                .ReadFrom.Configuration(builder.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console(new ExpressionTemplate("[{@t:HH:mm:ss} {@l:u3}{#if @tr is not null} ({substring(@tr,0,4)}:{substring(@sp,0,4)}){#end}] {@m}\n{@x}", theme: TemplateTheme.Code))
            );
        }
    }
}