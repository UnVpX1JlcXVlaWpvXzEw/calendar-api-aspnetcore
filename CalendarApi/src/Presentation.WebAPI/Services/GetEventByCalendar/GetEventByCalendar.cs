namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetEventByCalendar
{
    using AutoMapper;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using HustleAddiction.Platform.CalendarApi.Domain.Services.EventOccurrenceService;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;

    public class GetEventByCalendar : IGetEventByCalendar
    {
        private readonly ICalendarRepository calendarRepository;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;
        private readonly IEventOccurrenceService eventOccurrenceService;
        private readonly IMapper mapper;

        public GetEventByCalendar(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            calendarRepository = provider.GetRequiredService<ICalendarRepository>();
            eventOccurrenceService = provider.GetRequiredService<IEventOccurrenceService>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
            mapper = provider.GetRequiredService<IMapper>();
        }

        public async Task<List<EventSummary>> GetEventSummariesAsync(
            Guid calendarId,
            GetEventOcurrencesRequest request,
            CancellationToken cancellationToken = default)
        {
            var ownerId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = await calendarRepository.GetAsync(calendarId, cancellationToken)
                ?? throw new KeyNotFoundException($"Calendar {calendarId} not found.");

            if (calendar.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You are not authorized to access this calendar.");

            var interval = new DateRange(request.From, request.After);
            List<EventDetails> result = [];

            foreach (var calendarEvent in calendar.Events)
            {
                if (calendarEvent.Rule is not null)
                {
                    result.AddRange(eventOccurrenceService.Generate(calendarEvent, interval));

                    continue;
                }

                if (calendarEvent.DateRange.Overlaps(interval))
                {
                    result.Add(new EventDetails
                    {
                        EventId = calendarEvent.UUId,
                        Date = calendarEvent.DateRange.Start,
                        Title = calendarEvent.Title,
                        Location = calendarEvent.Location
                    });
                }
            }

            return mapper.Map<List<EventSummary>>(result);
        }
    }
}

