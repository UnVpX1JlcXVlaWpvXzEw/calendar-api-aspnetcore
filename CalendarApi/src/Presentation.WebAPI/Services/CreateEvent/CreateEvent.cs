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
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;

        public CreateEvent(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            eventRepository = provider.GetRequiredService<IEventRepository>();
            calendarRepository = provider.GetRequiredService<ICalendarRepository>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
        }

        public async Task<Guid> CreateAsync(
            Guid calendarId,
            CreateEventRequest request,
            CancellationToken cancellationToken = default)
        {
            var ownerId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = await calendarRepository.GetByIdAsync(calendarId, cancellationToken)
                ?? throw new KeyNotFoundException("Calendar not found.");

            if (calendar.OwnerId != ownerId)
            {
                throw new UnauthorizedAccessException("You are not authorized to add an event on this calendar.");
            }

            var newEvent = new Event
            {
                Title = request.Title,
                Description = request.Description,
                StartTime = request.StartDate,
                EndTime = request.EndDate,
                Location = request.Location,
            };

            calendar.AddEvent(newEvent);

            await eventRepository.AddAsync(newEvent, cancellationToken);
            await eventRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return newEvent.UUId;
        }
    }
}
