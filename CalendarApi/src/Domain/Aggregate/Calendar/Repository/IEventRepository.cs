using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetByOwnerIdAsync(Guid ownerId, CancellationToken token);
        Task<IEnumerable<Event>> GetEventsInRangeAsync(Guid ownerId, DateTime start, DateTime end, CancellationToken token);
    }
}