namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetEvent
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using HustleAddiction.Platform.CalendarApi.Domain.Services.EventOccurrenceService;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;
    using Microsoft.AspNetCore.Mvc;

    public class GetEventByCalendar : IGetEventByCalendar
    {
        private readonly ICalendarRepository calendarRepository;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;
        private readonly IEventOccurrenceService eventOccurrenceService;

        public GetEventByCalendar(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            calendarRepository = provider.GetRequiredService<ICalendarRepository>();
            eventOccurrenceService = provider.GetRequiredService<IEventOccurrenceService>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
        }

        public async Task<List<EventDetails>> GetEventSummariesAsync( //Trocar para lista Event
            Guid calendarId,
            [FromQuery] GetEventOcurrencesRequest request,
            CancellationToken cancellationToken = default)
        {
            var ownerId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = await calendarRepository.GetAsync(calendarId, cancellationToken);

            if (calendar.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You are not authorized to access this calendar.");

            var interval = new DateRange(request.From, request.After);
            var result = new List<EventDetails>();

            foreach (var e in calendar.Events)
            {
                var rules = e.Rules ?? Enumerable.Empty<RecurrenceRule>();
                if (!rules.Any())
                {
                    if (e.DateRange is not null && e.DateRange.Overlaps(interval))
                    {
                        result.Add(new EventDetails
                        {
                            Date = e.DateRange.Start,
                            Title = e.Title,
                            Location = e.Location
                        });
                    }

                    continue;
                }

                result.AddRange(eventOccurrenceService.Generate(e, request.From, request.After));
            }
            return result;
        }
    }
}

