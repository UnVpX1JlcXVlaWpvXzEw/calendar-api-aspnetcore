using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
using Microsoft.EntityFrameworkCore;

namespace HustleAddiction.Platform.CalendarApi.Infrastructure.Repository
{
    public class EventRepository(CalendarAPIDbContext context)
        : GenericRepository<Event>(context),
        IEventRepository
    {
        public async Task<IEnumerable<Event>> GetByOwnerIdAsync(CancellationToken cancellationToken = default)
        {
            return await this.Entities
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetEventsInRangeAsync(
            DateTime start,
            DateTime end,
            CancellationToken cancellationToken = default)
        {
            return await this.Entities
                .Where(e =>
                    e.DateRange.Start >= start &&
                    e.DateRange.End <= end)
                .ToListAsync(cancellationToken);
        }
    }
}
