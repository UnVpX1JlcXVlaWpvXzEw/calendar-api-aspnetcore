namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteCalendar
{
    public interface IDeleteCalendar
    {
        Task DeleteAsync(Guid calendarId, CancellationToken cancellationToken);
    }
}
