using System.Text.Json;

using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;

using api.Data;

using Serilog;
using Serilog.Templates;
using Serilog.Templates.Themes;
using api.Interfaces;
using api.Repositories;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;

try
{
  var builder = WebApplication.CreateBuilder(args);

  builder.Services.AddScoped<IStockRepository, StockRepository>();
  builder.Services.AddScoped<ICommentRepository, CommentRepository>();

  builder.Services.ConfigureHttpJsonOptions(options =>
  {
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
  });
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

  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();

  builder.Services.AddControllers()
  .AddNewtonsoftJson(options =>
  {
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
  });

  builder.Services.AddDbContext<ApplicationDBContext>(options =>
  {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
  });

  var app = builder.Build();

  if (!app.Environment.IsDevelopment())
  {
    app.UseExceptionHandler("/error");
  }
  else
  {
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
  }

  app.UseHttpLogging();
  app.UseSerilogRequestLogging();

  app.UseRouting();

  app.MapControllers();

  app.MapGet("/error", () => Results.Problem("An error occurred.", statusCode: StatusCodes.Status500InternalServerError));

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