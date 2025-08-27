namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.NotificationDeliveryJob
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob.Repository;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.ExecutePendingJobs;

    public class NotificationDeliveryJob
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
            var due = await notificationJobRepository.GetPendingAsync(DateTime.UtcNow, cancellationToken);

            foreach (var job in due)
            {
                var request = new ExecutePendingJobsRequest
                {
                    CalendarId = job.CalendarId,
                    NotificationJobId = job.UUId
                };

                await executePendingJobs.ExecuteAsync(request, cancellationToken);
            }
        }
    }
}


