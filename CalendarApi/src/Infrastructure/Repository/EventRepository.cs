using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
using Microsoft.EntityFrameworkCore;

namespace HustleAddiction.Platform.CalendarApi.Infrastructure.Repository
{
    public class EventRepository(CalendarAPIDbContext context) : GenericRepository<Event>(context), IEventRepository
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
                    e.StartTime >= start &&
                    e.EndTime <= end)
                .ToListAsync(cancellationToken);
        }

        public async Task<Guid> AddWithCalendarAsync(Event calendarEvent, Guid calendarUuId, CancellationToken cancellationToken = default)
        {
            context.Entry(calendarEvent)
                   .Property("CalendarId")
                   .CurrentValue = calendarUuId;

            await context.AddAsync(calendarEvent, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return calendarEvent.UUId;
        }
    }
}
