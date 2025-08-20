namespace HustleAddiction.Platform.CalendarApi.Infrastructure.Repository
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob.Repository;

    internal class NotificationJobRepository(CalendarAPIDbContext context)
        : GenericRepository<NotificationJob>(context),
        INotificationJobRepository
    {
    }
}
