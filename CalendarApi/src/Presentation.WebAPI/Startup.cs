namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Cors.Configuration;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Exception.Middleware;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void Configure(WebApplication app)
        {
            app.UseCors(this.Configuration);

            app.UseExceptionMiddleware();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API V1");
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}
