namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.UpdateEvent
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;

    public interface IUpdateEvent
    {
        Task UpdateEventAsync(
            Guid calendarId,
            Guid eventId,
            UpdateEventRequest request,
            CancellationToken cancellationToken);
    }
}
