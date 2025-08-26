namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.RescheduleNotificationJob
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;

    public interface IRescheduleNotificationJob
    {
        Task RescheduleAsync(
            RescheduleNotificationJobRequest request,
            CancellationToken cancellationToken);
    }
}
