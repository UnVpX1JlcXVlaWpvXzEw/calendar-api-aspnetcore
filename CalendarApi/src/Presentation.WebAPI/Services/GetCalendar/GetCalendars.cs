namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetCalendar
{
    using AutoMapper;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;

    public class GetCalendars : IGetCalendars
    {
        private readonly ICalendarRepository calendarRepository;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;
        private readonly IMapper mapper;

        public GetCalendars(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            calendarRepository = provider.GetRequiredService<ICalendarRepository>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();

            mapper = provider.GetRequiredService<IMapper>();
        }

        public async Task<List<CalendarSummary>> GetAsync(CancellationToken cancellationToken = default)
        {
            var ownerId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendars = await calendarRepository
                .GetByOwnerIdAsync(ownerId, cancellationToken);

            return mapper.Map<List<CalendarSummary>>(calendars);
        }
    }
}
