namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.DeleteReminder
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;

    public class DeleteReminder : IDeleteReminder
    {
        private readonly ICalendarRepository calendarRepository;
        private readonly IEventRepository eventRepository;
        private readonly IReminderRepository reminderRepository;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;
        public DeleteReminder(IServiceProvider provider)
        {
            calendarRepository = provider.GetRequiredService<ICalendarRepository>();
            eventRepository = provider.GetRequiredService<IEventRepository>();
            reminderRepository = provider.GetRequiredService<IReminderRepository>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
        }
        public async Task DeleteReminderAsync(
            Guid calendarId,
            Guid eventId,
            Guid reminderId,
            CancellationToken cancellationToken)
        {
            var ownerId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = await calendarRepository.GetAsync(calendarId, cancellationToken)
                ?? throw new KeyNotFoundException("Calendar not found.");

            if (calendar.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You are not authorized to delete this calendar.");

            var eventToUpdate = calendar.Events.FirstOrDefault(x => x.UUId == eventId)
                ?? throw new KeyNotFoundException("Event not found");

            var reminder = eventToUpdate.Reminders.FirstOrDefault(r => r.UUId == reminderId)
                ?? throw new KeyNotFoundException("Reminder not found");

            eventToUpdate.RemoveReminder(reminder);

            await reminderRepository.Remove(reminder, cancellationToken);
            await eventRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
