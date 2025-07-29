using HustleAddiction.Platform.CalendarApi.Domain;
using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
using HustleAddiction.Platform.CalendarApi.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace HustleAddiction.Platform.CalendarApi.Infrastructure.Repository
{
    public class CalendarRepository : GenericRepository<Calendar>, ICalendarRepository
    {
        public CalendarRepository(CalendarAPIDbContext context) : base(context)
        {
        }

        public async Task<Calendar?> GetByOwnerIdAsync(
            Guid ownerId,
            CancellationToken token)
        {
            return await this.Entities
                .FirstOrDefaultAsync(
                    c => c.OwnerId == ownerId, token);
        }
    }
}
