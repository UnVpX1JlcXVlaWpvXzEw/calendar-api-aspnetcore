namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetEvent
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;

    public interface IGetEventByCalendar
    {
        Task<List<EventSummary>> GetEventSummariesAsync(
            Guid calendarId,
            GetEventOcurrencesRequest request,
            CancellationToken cancellationToken = default);
    }
}
