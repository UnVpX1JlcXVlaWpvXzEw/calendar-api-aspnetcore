namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteCalendar
{
    public interface IDeleteCalendar
    {
        Task<Guid> DeleteAsync(Guid guid, Guid ownerId, CancellationToken cancellationToken);
    }
}
