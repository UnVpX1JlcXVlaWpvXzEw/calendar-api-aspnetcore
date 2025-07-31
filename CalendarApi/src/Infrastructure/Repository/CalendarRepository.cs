using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
using Microsoft.EntityFrameworkCore;

namespace HustleAddiction.Platform.CalendarApi.Infrastructure.Repository
{
    public class CalendarRepository(CalendarAPIDbContext context) : GenericRepository<Calendar>(context), ICalendarRepository
    {
        public async Task<Calendar?> GetByOwnerIdAsync(
            Guid ownerId,
            CancellationToken cancellationToken = default)
        {
            return await this.Entities
                .FirstOrDefaultAsync(
                    c => c.OwnerId == ownerId,
                    cancellationToken);
        }
    }
}
