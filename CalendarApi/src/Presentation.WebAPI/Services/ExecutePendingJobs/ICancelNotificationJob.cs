namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.ExecutePendingJobs
{
    public interface ICancelNotificationJob
    {
        Task CancelNotificationJobAsync(
            Guid calendarId,
            Guid notificationJobId,
            CancellationToken cancellationToken);
    }
}
