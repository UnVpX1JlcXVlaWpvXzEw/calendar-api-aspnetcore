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

        public CreateEvent(IServiceProvider serviceProvider)
        {
            eventRepository = serviceProvider.GetRequiredService<IEventRepository>();
            calendarRepository = serviceProvider.GetRequiredService<ICalendarRepository>();
            currentUserInfoProvider = serviceProvider.GetRequiredService<ICurrentUserInfoProvider>();
        }

        public async Task<Guid> CreateAsync(
            Guid calendarId,
            CreateEventRequest request,
            CancellationToken cancellationToken = default)
        {
            var ownerId = await currentUserInfoProvider
                .GetUserId(cancellationToken);

            var calendar = await calendarRepository
                .GetByUuIdAsync(calendarId, cancellationToken)
                ?? throw new InvalidOperationException("Calendar not found.");


            var newEvent = new Event
            {
                Title = request.Title,
                Description = request.Description,
                StartTime = request.StartDate,
                EndTime = request.EndDate,
                Location = request.Location,
            };

            var eventId = await eventRepository.AddWithCalendarAsync(
                newEvent,
                calendar.UUId,
                cancellationToken);

            return eventId;

        }
    }
}
