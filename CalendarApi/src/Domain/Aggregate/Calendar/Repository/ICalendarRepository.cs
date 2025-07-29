namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public interface ICalendarRepository : IRepository<Calendar>
    {
        Task<Calendar?> GetByOwnerIdAsync(Guid ownerId, CancellationToken token);
    }
}
