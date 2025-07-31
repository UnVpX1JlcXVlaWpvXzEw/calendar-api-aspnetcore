namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateCalendar
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;

    public interface ICreateCalendar
    {
        Task<CreateCalendarResponse> CreateAsync(
            string name,
            Guid ownerId,
            CancellationToken cancellation);
    }
}
