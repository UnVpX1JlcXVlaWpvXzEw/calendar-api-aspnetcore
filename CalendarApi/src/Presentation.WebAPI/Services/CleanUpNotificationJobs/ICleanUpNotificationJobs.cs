namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CleanUpNotificationJobs
{
    public interface ICleanUpNotificationJobs
    {
        Task CleanAsync(CancellationToken cancellationToken);
    }
}
