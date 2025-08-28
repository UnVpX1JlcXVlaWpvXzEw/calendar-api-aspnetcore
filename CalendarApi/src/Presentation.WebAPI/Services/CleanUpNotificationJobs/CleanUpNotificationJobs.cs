namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CleanUpNotificationJobs
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob.Repository;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CancelNotificationJob;

    public class CleanUpNotificationJobs : ICleanUpNotificationJobs
    {
        private readonly INotificationJobRepository notificationJobRepository;
        private readonly ICancelNotificationJob cancelNotificationJob;

        public CleanUpNotificationJobs(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            notificationJobRepository = provider.GetRequiredService<INotificationJobRepository>();
            cancelNotificationJob = provider.GetRequiredService<ICancelNotificationJob>();
        }

        public async Task CleanAsync(CancellationToken cancellationToken)
        {
            var toDelete = await notificationJobRepository
                .GetByStatusAsync(Status.SENT, cancellationToken);

            foreach (var job in toDelete)
            {
                await notificationJobRepository.Remove(job, cancellationToken);
            }

            await notificationJobRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
