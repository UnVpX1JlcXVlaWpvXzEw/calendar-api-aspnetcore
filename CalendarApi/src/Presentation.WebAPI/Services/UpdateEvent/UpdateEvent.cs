namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.UpdateEvent
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;

    public class UpdateEvent : IUpdateEvent
    {
        private readonly ICalendarRepository calendarRepository;
        private readonly IEventRepository eventRepository;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;

        public UpdateEvent(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            calendarRepository = provider.GetRequiredService<ICalendarRepository>();
            eventRepository = provider.GetRequiredService<IEventRepository>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
        }

        public async Task UpdateEventAsync(
            Guid calendarId,
            Guid eventId,
            UpdateEventRequest request,
            CancellationToken cancellationToken)
        {
            var ownerId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = await calendarRepository.GetAsync(calendarId, cancellationToken)
                ?? throw new KeyNotFoundException("Calendar not found.");

            if (calendar.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You are not authorized to delete this calendar.");

            var eventToUpdate = calendar.Events.FirstOrDefault(x => x.UUId == eventId)
                ?? throw new KeyNotFoundException("Event not found");

            eventToUpdate.Title = request.Title;
            eventToUpdate.Description = request.Description;

            eventToUpdate.DateRange = new DateRange(
                request.StartTime.UtcDateTime,
                request.EndTime.UtcDateTime);

            foreach (var r in request.Reminders)
            {
                var reminder = new Reminder
                {
                    OffsetInMinutes = r.OffsetInMinutes,
                    Method = (ReminderMethod?)r.Method,
                    Enabled = r.Enabled
                };

                eventToUpdate.AddReminder(reminder);
            }

            await eventRepository.Update(eventToUpdate, cancellationToken);
            await eventRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
