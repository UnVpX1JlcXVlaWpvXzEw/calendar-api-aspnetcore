namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteCalendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;

    public class DeleteCalendar : IDeleteCalendar
    {
        private readonly ICalendarRepository calendarRepository;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;

        public DeleteCalendar(IServiceProvider provider)
        {
            calendarRepository = provider.GetRequiredService<ICalendarRepository>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
        }

        public async Task DeleteCalendarAsync(
            Guid calendarId,
            CancellationToken cancellationToken)
        {
            var ownerId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = await calendarRepository.GetAsync(calendarId, cancellationToken)
                ?? throw new KeyNotFoundException("Calendar not found.");

            if (calendar.OwnerId != ownerId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this calendar.");
            }

            await calendarRepository.Remove(calendar, cancellationToken);
            await calendarRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
