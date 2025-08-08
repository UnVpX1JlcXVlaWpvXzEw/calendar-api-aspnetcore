namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetEvent
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;

    public interface IGetEventOccurrences
    {
        Task<List<EventSummary>> GetEventSummariesAsync(
            Guid calendarId,
            DateTime from,
            DateTime after,
            CancellationToken cancellationToken = default);
    }
}
