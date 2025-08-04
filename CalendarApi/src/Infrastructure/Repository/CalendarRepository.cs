using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
using Microsoft.EntityFrameworkCore;

namespace HustleAddiction.Platform.CalendarApi.Infrastructure.Repository
{
    public class CalendarRepository(CalendarAPIDbContext context) : GenericRepository<Calendar>(context), ICalendarRepository
    {
        public async Task<List<Calendar>> GetByOwnerIdAsync(
            Guid ownerId,
            CancellationToken cancellationToken = default)
        {
            return await this.Entities
                .Where(c => c.OwnerId == ownerId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Calendar?> GetByIdAsync(
            Guid calendarId,
            CancellationToken cancellationToken = default)
        {
            return await this.Entities
                .FirstOrDefaultAsync(c =>
                    c.UUId == calendarId,
                    cancellationToken);
        }
    }
}
