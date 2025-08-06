namespace HustleAddiction.Platform.CalendarApi.Infrastructure.Repository
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using Microsoft.EntityFrameworkCore;

    public class ReminderRepository(CalendarAPIDbContext context)
        : GenericRepository<Reminder>(context), IReminderRepository
    {
        public async Task<IEnumerable<Reminder>> GetByEventIdAsync(
            Guid eventId,
            CancellationToken cancellationToken = default)
        {
            return await this.Entities
                .ToListAsync(cancellationToken);
        }
    }
}
