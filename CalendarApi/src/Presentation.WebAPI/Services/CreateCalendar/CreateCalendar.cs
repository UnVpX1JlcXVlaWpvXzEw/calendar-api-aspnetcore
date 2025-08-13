namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateCalendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;

    public class CreateCalendar : ICreateCalendar
    {
        private readonly ICalendarRepository calendarRepository;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;

        public CreateCalendar(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            calendarRepository = provider.GetRequiredService<ICalendarRepository>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
        }

        public async Task<Guid> CreateAsync(
            CreateCalendarRequest request,
            CancellationToken cancellationToken = default)
        {
            var ownerId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = new Calendar
            {
                Name = request.Name,
                OwnerId = ownerId
            };

            await calendarRepository.AddAsync(calendar, cancellationToken);
            await calendarRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return calendar.UUId;
        }
    }
}
