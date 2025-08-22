namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.ExecutePendingJobs
{
    public interface IExecutePendingJobs
    {
        Task ExecutePendingJobsAsync(
            Guid calendarId,
            Guid notificationJobId,
            CancellationToken cancellationToken);
    }
}
