namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public interface IReminderRepository : IRepository<Reminder>
    {
        Task<IEnumerable<Reminder>> GetByEventIdAsync(
            Guid eventId,
            CancellationToken cancellationToken = default!);
    }
}
