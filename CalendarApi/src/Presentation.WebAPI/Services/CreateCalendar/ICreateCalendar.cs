namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateCalendar
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;

    public interface ICreateCalendar
    {
        Task<CreateCalendarResponse> CreateAsync(
            CreateCalendarRequest request,
            CancellationToken cancellationToken);
    }
}
