namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.ScheduleNotificationJob
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;

    public interface IScheduleNotificationJob
    {
        Task ScheduleAsync(
            ScheduleNotificationJobRequest request,
            CancellationToken cancellationToken);
    }
}
