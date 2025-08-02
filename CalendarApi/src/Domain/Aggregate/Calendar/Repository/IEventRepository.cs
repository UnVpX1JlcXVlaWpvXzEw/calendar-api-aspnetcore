using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetByOwnerIdAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<Event>> GetEventsInRangeAsync(
            DateTime start,
            DateTime end,
            CancellationToken cancellationToken = default);

        Task<Guid> AddWithCalendarAsync(
            Event calendarEvent,
            Guid calendarUuId,
            CancellationToken cancellationToken = default);
    }
}