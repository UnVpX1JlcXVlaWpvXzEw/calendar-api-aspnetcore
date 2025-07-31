namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Configuration
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateCalendar;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.DateTimeProvider;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Jwt.Common;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollection
    {
        public static void RegisterPresentationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

            services
                .AddSingleton<IDateTimeProvider, DateTimeProvider>()
                .AddScoped<ICreateCalendar, CreateCalendar>();
        }
    }
}
