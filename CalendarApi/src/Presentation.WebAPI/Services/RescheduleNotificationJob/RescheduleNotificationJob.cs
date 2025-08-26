namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.RescheduleNotificationJob
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob.Repository;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;

    public class RescheduleNotificationJob : IRescheduleNotificationJob
    {
        private readonly ICalendarRepository calendarRepository;
        private readonly IEventRepository eventRepository;
        private readonly INotificationJobRepository notificationJobRepository;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;

        public RescheduleNotificationJob(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            notificationJobRepository = provider.GetRequiredService<INotificationJobRepository>();
            calendarRepository = provider.GetRequiredService<ICalendarRepository>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
            eventRepository = provider.GetRequiredService<IEventRepository>();
        }

        public async Task RescheduleAsync(
            RescheduleNotificationJobRequest request,
            CancellationToken cancellationToken)
        {
            var userId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = await calendarRepository.GetAsync(request.CalendarId, cancellationToken)
                ?? throw new KeyNotFoundException("Calendar not found.");

            if (calendar.OwnerId != userId)
                throw new UnauthorizedAccessException("You are not authorized to delete this calendar.");

            var job = await notificationJobRepository.GetAsync(request.NotificationJobId, cancellationToken)
                ?? throw new KeyNotFoundException("Notification job not found.");

            if (job.CalendarId != calendar.UUId)
                throw new KeyNotFoundException("Notification job does not belong to the specified calendar.");

            var selectedEvent = await eventRepository.GetAsync(request.EventId, cancellationToken)
               ?? throw new KeyNotFoundException("Event not found.");

            var eventStart = selectedEvent.DateRange?.Start
                ?? throw new InvalidOperationException("Event has no start time.");

            if (request.ReminderOffset.HasValue)
            {
                job.ReminderOffset = request.ReminderOffset.Value;

                job.ValidateOffset();
                job.CalculateScheduledTime(eventStart);
            }

            await notificationJobRepository.Update(job, cancellationToken);
            await notificationJobRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
