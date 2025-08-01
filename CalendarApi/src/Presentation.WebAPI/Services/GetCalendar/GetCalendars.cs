namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetCalendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
    using HustleAddiction.Platform.CalendarApi.Infrastructure;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;
    using Microsoft.EntityFrameworkCore;

    public class GetCalendars : IGetCalendars
    {
        private readonly CalendarAPIDbContext context;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;

        public GetCalendars(IServiceProvider provider)
        {
            context = provider.GetRequiredService<CalendarAPIDbContext>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
        }

        public async Task<List<CalendarSummary>> GetAsync(CancellationToken cancellationToken)
        {
            var ownerId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendars = await context
                .Set<Calendar>()
                .Where(c => c.OwnerId == ownerId)
                .ToListAsync(cancellationToken);

            return calendars.Select(c => new CalendarSummary
            {
                Id = c.UUId,
                Name = c.Name,
                OwnerId = c.OwnerId,
            }).ToList();
        }
    }
}
