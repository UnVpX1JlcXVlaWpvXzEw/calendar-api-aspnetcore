namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteEvent
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;

    public class DeleteEvent : IDeleteEvent
    {
        private readonly ICalendarRepository calendarRepository;
        private readonly IEventRepository eventRepository;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;

        public DeleteEvent(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            calendarRepository = provider.GetRequiredService<ICalendarRepository>();
            eventRepository = provider.GetRequiredService<IEventRepository>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
        }

        public async Task DeleteEventAsync(
            Guid calendarId,
            Guid eventId,
            CancellationToken cancellationToken)
        {
            var ownerId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = await calendarRepository.GetAsync(calendarId, cancellationToken)
                ?? throw new KeyNotFoundException("Calendar not found.");

            if (calendar.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You are not authorized to delete this calendar.");

            var eventToDelete = calendar.Events.FirstOrDefault(x => x.UUId == eventId)
                ?? throw new KeyNotFoundException("Event not found.");

            await eventRepository.Remove(eventToDelete, cancellationToken);
            await eventRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
