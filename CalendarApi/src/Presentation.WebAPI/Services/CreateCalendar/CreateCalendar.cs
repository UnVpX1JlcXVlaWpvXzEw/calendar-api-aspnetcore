namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.CreateCalendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
    using HustleAddiction.Platform.CalendarApi.Infrastructure;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;

    public class CreateCalendar : ICreateCalendar
    {
        private readonly CalendarAPIDbContext context;

        public CreateCalendar(IServiceProvider provider)
        {
            context = provider.GetRequiredService<CalendarAPIDbContext>();
        }

        public async Task<CreateCalendarResponse> CreateAsync(
            string name,
            Guid ownerId,
            CancellationToken cancellationToken)
        {
            var calendar = new Calendar
            {
                Name = name,
                OwnerId = ownerId,
            };

            await context.Set<Calendar>().AddAsync(calendar);
            await context.SaveEntitiesAsync(cancellationToken);

            return new CreateCalendarResponse
            {
                Id = calendar.UUId,
                Name = name,
                OwnerId = calendar.OwnerId
            };
        }
    }
}
