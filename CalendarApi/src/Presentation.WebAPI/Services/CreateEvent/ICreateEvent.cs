namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateEvent
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;

    public interface ICreateEvent
    {
        Task<Guid> CreateAsync(
            Guid calendarId,
            CreateEventRequest request,
            CancellationToken cancellationToken = default);
    }
}
