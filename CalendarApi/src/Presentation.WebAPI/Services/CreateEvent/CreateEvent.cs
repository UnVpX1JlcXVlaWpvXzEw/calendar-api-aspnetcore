namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateEvent
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;

    public class CreateEvent : ICreateEvent
    {
        private readonly ICalendarRepository calendarRepository;
        private readonly IEventRepository eventRepository;
        private readonly IRecurrenceRuleRepository recurrenceRuleRepository;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;

        public CreateEvent(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            eventRepository = provider.GetRequiredService<IEventRepository>();
            calendarRepository = provider.GetRequiredService<ICalendarRepository>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
            recurrenceRuleRepository = provider.GetRequiredService<IRecurrenceRuleRepository>();
        }

        public async Task<Guid> CreateAsync(
            Guid calendarId,
            CreateEventRequest request,
            CancellationToken cancellationToken = default)
        {
            var ownerId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = await calendarRepository.GetAsync(calendarId, cancellationToken)
                ?? throw new KeyNotFoundException("Calendar not found.");

            if (calendar.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You are not authorized to add an event on this calendar.");

            if (request.DateRange is null)
                throw new ArgumentException("DateRange is required.");

            var newEvent = new Event
            {
                Title = request.Title,
                Description = request.Description,
                DateRange = request.DateRange,
                Location = request.Location
            };

            if (request.RecurrenceRule is not null)
            {
                var recurrenceRequest = request.RecurrenceRule;

                var domainRule = new RecurrenceRule
                {
                    Frequency = recurrenceRequest.Frequency,
                    Start = recurrenceRequest.Start,
                    Count = recurrenceRequest.Count,
                    Until = recurrenceRequest.Until,
                };
                await recurrenceRuleRepository.AddAsync(domainRule, cancellationToken);
                newEvent.AddRules(domainRule);
            }

            calendar.AddEvent(newEvent);

            await eventRepository.AddAsync(newEvent, cancellationToken);

            await eventRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return newEvent.UUId;
        }
    }
}
