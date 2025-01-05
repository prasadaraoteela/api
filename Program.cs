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
    new IdentityStartup()
  };

  try
  {
    startups.ForEach(startup => startup.ConfigureBuilder(builder));
  }
  catch (Exception ex)
  {
    Console.WriteLine($"{ex.Message}");
    throw;
  }

  var app = builder.Build();
  try
  {
    startups.ForEach(startup => startup.Configure(app));
  }
  catch (Exception ex)
  {
    Console.WriteLine($"{ex.Message}");
    throw;
  }
  app.Run();
}
catch (Exception ex)
{
  Console.WriteLine($"{ex.Message}");
  Log.Fatal(ex, "Application terminated unexpectedly");
  throw; // Re-throw the exception to get more details in the logs
}
finally
{
  Log.CloseAndFlush();
}