namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Configuration
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateCalendar;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateEvent;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteCalendar;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteEvent;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteReminder;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetCalendar;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetEventByCalendar;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.ScheduleNotificationJob;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.RescheduleNotificationJob;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.UpdateEvent;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.DateTimeProvider;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Jwt.Common;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class ServiceCollection
    {
        public static void RegisterPresentationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

            services
                .TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services
                .AddSingleton<IDateTimeProvider, DateTimeProvider>()
                .AddScoped<ICurrentUserInfoProvider, CurrentUserInfoProvider>()
                .AddScoped<ICreateCalendar, CreateCalendar>()
                .AddScoped<IGetCalendars, GetCalendars>()
                .AddScoped<ICreateEvent, CreateEvent>()
                .AddScoped<IDeleteCalendar, DeleteCalendar>()
                .AddScoped<IDeleteEvent, DeleteEvent>()
                .AddScoped<IUpdateEvent, UpdateEvent>()
                .AddScoped<IDeleteReminder, DeleteReminder>()
                .AddScoped<IGetEventByCalendar, GetEventByCalendar>()
                .AddScoped<IScheduleNotificationJob, ScheduleNotificationJob>();
        }
    }
}
