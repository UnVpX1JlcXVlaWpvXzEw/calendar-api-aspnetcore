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
            eventRepository = provider.GetRequiredService<IEventRepository>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
        }

        public async Task UpdateNotificationJob(
            Guid calendarId,
            Guid notificationJobId,
            UpdateNotificationJobRequest request,
            CancellationToken cancellationToken)
        {
            var ownerId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = await calendarRepository.GetAsync(calendarId, cancellationToken)
                ?? throw new KeyNotFoundException("Calendar not found.");

            if (calendar.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You are not authorized to delete this calendar.");

            var job = await notificationJobRepository.GetAsync(notificationJobId, cancellationToken)
                ?? throw new KeyNotFoundException("Notification job not found.");

            if (job.CalendarId != calendar.UUId)
                throw new KeyNotFoundException("Notification job does not belong to the specified calendar.");


            if (request.ReminderOffset.HasValue)
            {
                job.ReminderOffset = request.ReminderOffset.Value;

                job.ValidateOffset();
                job.CalculateScheduledTime();
            }

            if (request.Channel.HasValue)
                job.Channel = request.Channel.Value;

            if (request.Status.HasValue)
                job.Status = request.Status.Value;

            await notificationJobRepository.Update(job, cancellationToken);
            await notificationJobRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
