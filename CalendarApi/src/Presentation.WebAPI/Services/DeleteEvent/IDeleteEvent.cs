namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteEvent
{
    public interface IDeleteEvent
    {
        Task DeleteEventAsync(
            Guid calendarId,
            Guid eventId,
            CancellationToken cancellationToken);
    }
}
