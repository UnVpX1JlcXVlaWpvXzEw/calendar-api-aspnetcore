namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI
{
    using HustleAddiction.Platform.CalendarApi.Infrastructure;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Cors.Configuration;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Exception.Middleware;
    using Microsoft.EntityFrameworkCore;

    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void Configure(WebApplication app)
        {
            MigrateDatabase(app);

            app.UseCors(this.Configuration);

            app.UseExceptionMiddleware();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Calendar API V1");
            });

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CalendarAPIDbContext>(options =>
                options.UseLazyLoadingProxies(),
                ServiceLifetime.Scoped);
        }

        private static void MigrateDatabase(WebApplication app)
        {
            using var scope = app.Services
                .CreateScope();

            var context = scope.ServiceProvider
                .GetRequiredService<CalendarAPIDbContext>();

            context.Database.MigrateAsync()
                .GetAwaiter()
                .GetResult();
        }
    }
}
