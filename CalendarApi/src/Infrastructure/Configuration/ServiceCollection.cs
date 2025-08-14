namespace HustleAddiction.Platform.CalendarApi.Infrastructure.Configuration
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using HustleAddiction.Platform.CalendarApi.Infrastructure.Repository;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollection
    {
        public static void RegisterInfraredServices(this IServiceCollection services)
        {
            services.AddScoped<ICalendarRepository, CalendarRepository>();

            services.AddScoped<IEventRepository, EventRepository>();

            services.AddScoped<IReminderRepository, ReminderRepository>();

            services.AddScoped<IRecurrenceRuleRepository, RecurrenceRuleRepository>();
        }
    }
}
