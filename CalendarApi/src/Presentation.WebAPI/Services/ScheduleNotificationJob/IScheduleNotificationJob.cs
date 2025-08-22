namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.ScheduleNotificationJob
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;

    public interface IScheduleNotificationJob
    {
        Task<Guid> CreateAsync(
            Guid calendarId,
            Guid eventId,
            CreateNotificationJobRequest request,
            CancellationToken cancellationToken);
    }
}
