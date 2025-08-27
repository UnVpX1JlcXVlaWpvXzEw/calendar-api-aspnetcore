namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob.Repository
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;
    using System.Threading.Tasks;

    public interface INotificationJobRepository : IRepository<NotificationJob>
    {
        Task<IReadOnlyList<NotificationJob>> GetPendingAsync(
            DateTime dateTime,
            CancellationToken cancellationToken);

        Task<IReadOnlyList<NotificationJob>> GetByStatusAsync(
            Status status,
            CancellationToken cancellationToken);
    }
}
