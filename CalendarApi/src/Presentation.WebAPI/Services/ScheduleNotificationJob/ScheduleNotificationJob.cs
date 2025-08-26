namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.ScheduleNotificationJob
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob.Repository;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;

    public class ScheduleNotificationJob : IScheduleNotificationJob
    {
        private readonly ICalendarRepository calendarRepository;
        private readonly IEventRepository eventRepository;
        private readonly INotificationJobRepository notificationJobRepository;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;

        public ScheduleNotificationJob(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            notificationJobRepository = provider.GetRequiredService<INotificationJobRepository>();
            calendarRepository = provider.GetRequiredService<ICalendarRepository>();
            eventRepository = provider.GetRequiredService<IEventRepository>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
        }

        public async Task ScheduleAsync(
            ScheduleNotificationJobRequest request,
            CancellationToken cancellationToken)
        {
            var userId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = await calendarRepository.GetAsync(request.calendarId, cancellationToken)
                ?? throw new KeyNotFoundException("Calendar not found.");

            if (calendar.OwnerId != userId)
                throw new UnauthorizedAccessException("You are not authorized to add an event on this calendar.");

            var selectedEvent = await eventRepository.GetAsync(request.eventId, cancellationToken)
                ?? throw new KeyNotFoundException("Event not found.");

            var eventStart = selectedEvent.DateRange?.Start
                ?? throw new InvalidOperationException("Event has no start time.");

            var job = new NotificationJob
            {
                TargetUserId = userId,
                EventId = selectedEvent.UUId,
                CalendarId = calendar.UUId,
                ReminderOffset = request.ReminderOffset,
                Channel = request.Channel,
                Status = Status.PENDING
            };

            job.ValidateOffset();
            job.CalculateScheduledTime(eventStart);

            await notificationJobRepository.AddAsync(job, cancellationToken);

            await notificationJobRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
