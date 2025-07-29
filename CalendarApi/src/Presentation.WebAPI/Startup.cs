namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI
{
    using HustleAddiction.Platform.CalendarApi.Domain;
    using Microsoft.EntityFrameworkCore;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void Configure(WebApplication app)
        {
            MigrateDatabase(app);
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
