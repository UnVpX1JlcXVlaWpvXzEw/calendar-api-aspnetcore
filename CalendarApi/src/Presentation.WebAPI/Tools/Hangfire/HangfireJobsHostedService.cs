namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Hangfire
{
    using global::Hangfire;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CleanUpNotificationJobs;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.NotificationDeliveryJob;

    public class HangfireJobsHostedService : IHostedService
    {
        private readonly IRecurringJobManager recurringJobs;

        public HangfireJobsHostedService(IRecurringJobManager recurringJobs)
        {
            this.recurringJobs = recurringJobs;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            recurringJobs.AddOrUpdate<NotificationDeliveryJob>(
                "deliver-notifications",
                job => job.DeliveryAsync(CancellationToken.None),
                "*/1 * * * *");

            recurringJobs.AddOrUpdate<CleanUpNotificationJobs>(
                "cleanup-sent-notification-jobs",
                job => job.CleanAsync(CancellationToken.None),
                "59 23 * * *");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
