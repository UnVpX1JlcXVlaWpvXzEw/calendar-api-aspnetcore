namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.RescheduleNotificationJob
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;

    public interface IRescheduleNotificationJob
    {
        Task UpdateNotificationJob(
            UpdateNotificationJobRequest request,
            CancellationToken cancellationToken);
    }
}
