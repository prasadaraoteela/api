namespace api.Startup
{
    public interface IStartup
    {
        public void ConfigureBuilder(WebApplicationBuilder builder);
        public void Configure(WebApplication application);
    }
}