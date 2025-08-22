namespace HustleAddiction.Platform.CalendarApi.Infrastructure.Repository
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob.Repository;
    using Microsoft.EntityFrameworkCore;

    internal class NotificationJobRepository(CalendarAPIDbContext context)
        : GenericRepository<NotificationJob>(context),
        INotificationJobRepository
    {
        public async Task<IEnumerable<NotificationJob>> GetByOwnerIdAsync(CancellationToken cancellationToken = default)
        {
            return await this.Entities
                .ToListAsync(cancellationToken);
        }
    }
}
