namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteCalendar
{
    public interface IDeleteCalendar
    {
        Task DeleteCalendarAsync(Guid calendarId, CancellationToken cancellationToken);
    }
}
