using api.Startup;

var builder = WebApplication.CreateBuilder(args);

var startups = new List<api.Startup.IStartup>() {
    new DatabaseStartup(),
    new ControllerStartup(),
    new SwaggerStartup(),
    new JsonConfigurationStartup(),
    new LoggingStartup(),
    new ErrorHandlingStartup(),
    new IdentityStartup(),
    new DependencyInjection(),
  };

startups.ForEach(startup => startup.ConfigureBuilder(builder));

var app = builder.Build();
startups.ForEach(startup => startup.Configure(app));
app.Run();