namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetEvent
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using HustleAddiction.Platform.CalendarApi.Domain.Services.EventOccurrenceService;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;
    using Microsoft.AspNetCore.Mvc;

    public class GetEventOccurrences : IGetEventOccurrences
    {
        private readonly ICalendarRepository calendarRepository;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;
        private readonly IEventOccurrenceService eventOccurrenceService;

        public GetEventOccurrences(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            calendarRepository = provider.GetRequiredService<ICalendarRepository>();
            eventOccurrenceService = provider.GetRequiredService<IEventOccurrenceService>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
        }

        public async Task<List<EventOccurrence>> GetEventSummariesAsync(
            Guid calendarId,
            [FromQuery] GetEventOcurrencesRequest request,
            CancellationToken cancellationToken = default)
        {
            var ownerId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = await calendarRepository.GetAsync(calendarId, cancellationToken);

            if (calendar.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You are not authorized to access this calendar.");

            var list = new List<EventOccurrence>();
            foreach (var e in calendar.Events)
                list.AddRange(eventOccurrenceService.Generate(e, request.From, request.After));

            return list;
        }
    }
}

