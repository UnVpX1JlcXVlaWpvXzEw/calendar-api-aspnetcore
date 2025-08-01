namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateCalendar
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;

    public interface ICreateCalendar
    {
        Task<Guid> CreateAsync(
            CreateCalendarRequest request,
            CancellationToken cancellationToken);
    }
}
