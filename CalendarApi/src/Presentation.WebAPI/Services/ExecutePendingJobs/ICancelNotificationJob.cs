namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CancelNotificationJob
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;

    public interface ICancelNotificationJob
    {
        Task CancelAsync(
            CancelNotificationJobRequest request,
            CancellationToken cancellationToken);
    }
}
