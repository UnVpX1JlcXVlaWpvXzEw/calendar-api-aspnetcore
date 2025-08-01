namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateCalendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
    using HustleAddiction.Platform.CalendarApi.Infrastructure;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;

    public class CreateCalendar : ICreateCalendar
    {
        private readonly CalendarAPIDbContext context;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;

        public CreateCalendar(IServiceProvider provider)
        {
            context = provider.GetRequiredService<CalendarAPIDbContext>();
            currentUserInfoProvider = provider
                .GetRequiredService<ICurrentUserInfoProvider>();
        }

        public async Task<CreateCalendarResponse> CreateAsync(
            CreateCalendarRequest request,
            CancellationToken cancellationToken)
        {
            var ownerId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = new Calendar
            {
                Name = request.Name,
                OwnerId = ownerId
            };

            await context.Set<Calendar>().AddAsync(calendar, cancellationToken);
            await context.SaveEntitiesAsync(cancellationToken);

            return new CreateCalendarResponse
            {
                Id = calendar.UUId
            };
        }
    }
}
