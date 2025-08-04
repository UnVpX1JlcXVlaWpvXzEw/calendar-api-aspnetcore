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

        public async Task DeleteAsync(
            Guid calendarId,
            CancellationToken cancellationToken)
        {
            var ownerId = currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = await calendarRepository.GetByIdAsync(calendarId, cancellationToken)
                ?? throw new KeyNotFoundException("Calendar not found.");

            await calendarRepository.Remove(calendar, cancellationToken);

            await calendarRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
