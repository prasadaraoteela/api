namespace api.Startup
{
    public class SwaggerStartup : IStartup
    {
        public void Configure(WebApplication application)
        {
            if (application.Environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }
        }

        public void ConfigureBuilder(WebApplicationBuilder builder)
        {

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}