namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob.Repository
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;
    using System.Threading.Tasks;

    public interface INotificationJobRepository : IRepository<NotificationJob>
    {
        Task<IReadOnlyList<NotificationJob>> GetPendingAsync(
            DateTime dateTime,
            CancellationToken cancellationToken);
    }
}
