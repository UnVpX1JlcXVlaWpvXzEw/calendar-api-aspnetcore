namespace HustleAddiction.Platform.CalendarApi.Infrastructure.Repository
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob.Repository;
    using Microsoft.EntityFrameworkCore;

    internal class NotificationJobRepository(CalendarAPIDbContext context)
        : GenericRepository<NotificationJob>(context),
        INotificationJobRepository
    {
        public async Task<IReadOnlyList<NotificationJob>> GetPendingAsync(
            DateTime dateTime,
            CancellationToken cancellationToken)
        {
            return await this.Entities
                .Where(e =>
                    e.Status == Status.PENDING &&
                    e.ScheduledTime <= dateTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<NotificationJob>> GetByStatusAsync(
        Status status,
        CancellationToken cancellationToken)
        {
            return await this.Entities
                .Where(e => e.Status == status)
                .ToListAsync(cancellationToken);
        }
    }
}
