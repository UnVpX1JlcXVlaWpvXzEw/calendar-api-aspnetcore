namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.ExecutePendingJobs
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;

    public interface ICancelNotificationJob
    {
        Task CancelNotificationJobAsync(
            CalendarJobIdentifiersDto request,
            CancellationToken cancellationToken);
    }
}
