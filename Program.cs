using api.Startup;

using Serilog;

try
{
  var builder = WebApplication.CreateBuilder(args);

  var startups = new List<api.Startup.IStartup>() {
    new DependencyInjection(),
    new JsonConfigurationStartup(),
    new LoggingStartup(),
    new SwaggerStartup(),
    new ErrorHandlingStartup(),
    new DatabaseStartup(),
    new ControllerStartup(),
  };

  startups.ForEach(startup => startup.ConfigureBuilder(builder));

  var app = builder.Build();
  startups.ForEach(startup => startup.Configure(app));
  app.Run();
}
catch (Exception ex)
{
  Log.Fatal(ex, "Application terminated unexpectedly");
  throw; // Re-throw the exception to get more details in the logs
}
finally
{
  Log.CloseAndFlush();
}