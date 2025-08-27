namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.NotificationDeliveryJob
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob.Repository;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.ExecutePendingJobs;

    public class NotificationDeliveryJob : INotificationDeliveryJob
    {
        private readonly INotificationJobRepository notificationJobRepository;
        private readonly IExecutePendingJobs executePendingJobs;

        public NotificationDeliveryJob(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            notificationJobRepository = provider.GetRequiredService<INotificationJobRepository>();
            executePendingJobs = provider.GetRequiredService<IExecutePendingJobs>();
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var toDo = await notificationJobRepository
                .GetPendingAsync(DateTime.UtcNow, cancellationToken);

            foreach (var job in toDo)
            {
                if (
                    job.Status == Status.PENDING &&
                    job.ScheduledTime <= DateTime.UtcNow)
                {
                    job.Status = Status.SENT;
                    await notificationJobRepository.Update(job, cancellationToken);
                }
            }

            await notificationJobRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}


