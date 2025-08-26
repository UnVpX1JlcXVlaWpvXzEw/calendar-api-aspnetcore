namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.ExecutePendingJobs
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob.Repository;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;

    public class ExecutePendingJobs : IExecutePendingJobs
    {
        private readonly ICalendarRepository calendarRepository;
        private readonly INotificationJobRepository notificationJobRepository;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;

        public ExecutePendingJobs(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            notificationJobRepository = provider.GetRequiredService<INotificationJobRepository>();
            calendarRepository = provider.GetRequiredService<ICalendarRepository>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
        }

        public async Task ExecuteAsync(
            ExecutePendingJobsRequest request,
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

            if (job.Status != Status.PENDING)
                throw new InvalidOperationException("Only PENDING jobs can be executed.");

            if (job.ScheduledTime > DateTime.UtcNow)
                throw new InvalidOperationException("Job is not due yet.");

            job.Status = Status.SENT;

            await notificationJobRepository.Update(job, cancellationToken);
            await notificationJobRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

    }
}
