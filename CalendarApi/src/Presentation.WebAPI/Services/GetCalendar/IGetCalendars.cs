namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetCalendar
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;

    public interface IGetCalendars
    {
        Task<List<CalendarSummary>> GetAsync(CancellationToken cancellationToken = default);
    }
}
