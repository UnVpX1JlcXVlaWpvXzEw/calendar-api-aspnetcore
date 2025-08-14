using HustleAddiction.Platform.CalendarApi.Domain.Services.EventOccurrenceService;
using Microsoft.Extensions.DependencyInjection;

namespace HustleAddiction.Platform.CalendarApi.Domain.Configuration
{
    public static class ServiceCollection
    {
        public static void RegisterDomainServices(this IServiceCollection services)
        {
            services
                .AddScoped<IEventOccurrenceService, EventOccurrenceService>();
        }
    }
}
