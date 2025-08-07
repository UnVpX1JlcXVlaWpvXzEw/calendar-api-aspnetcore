namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteReminder
{
    public interface IDeleteReminder
    {
        Task DeleteReminderAsync(
            Guid calendarId,
            Guid eventId,
            Guid reminderId,
            CancellationToken cancellationToken);
    }
}
