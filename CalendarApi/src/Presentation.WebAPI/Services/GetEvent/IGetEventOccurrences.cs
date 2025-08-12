namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetEvent
{
    using HustleAddiction.Platform.CalendarApi.Domain.Services.EventOccurrenceService;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
    using Microsoft.AspNetCore.Mvc;

    public interface IGetEventOccurrences
    {
        Task<List<EventOccurrence>> GetEventSummariesAsync(
            Guid calendarId,
            [FromQuery] GetEventOcurrencesRequest request,
            CancellationToken cancellationToken = default);
    }
}
