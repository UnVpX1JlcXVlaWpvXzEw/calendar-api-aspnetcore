namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob.Repository
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public interface INotificationJobRepository : IRepository<NotificationJob>
    {
        Task<IEnumerable<NotificationJob>> GetByOwnerIdAsync(CancellationToken cancellationToken = default);
    }
}
