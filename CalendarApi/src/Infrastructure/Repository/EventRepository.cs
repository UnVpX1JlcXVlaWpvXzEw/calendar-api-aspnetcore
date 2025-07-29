using HustleAddiction.Platform.CalendarApi.Domain;
using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
using HustleAddiction.Platform.CalendarApi.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace HustleAddiction.Platform.CalendarApi.Infrastructure.Repository
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(CalendarAPIDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Event>> GetByOwnerIdAsync(
            Guid ownerId,
            CancellationToken token)
        {
            return await this.Entities
                .Where(e => e.OwnerId == ownerId)
                .ToListAsync(token);
        }

        public async Task<IEnumerable<Event>> GetEventsInRangeAsync(
            Guid ownerId,
            DateTime start,
            DateTime end,
            CancellationToken token)
        {
            return await this.Entities
                .Where(
                    e => e.OwnerId == ownerId &&
                    e.StartTime >= start &&
                    e.EndTime <= end)
                .ToListAsync(token);
        }
    }
}
