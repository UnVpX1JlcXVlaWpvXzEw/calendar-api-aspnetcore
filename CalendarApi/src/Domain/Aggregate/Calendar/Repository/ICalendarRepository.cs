namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public interface ICalendarRepository : IRepository<Calendar>
    {
        Task<List<Calendar>> GetByOwnerIdAsync(
            Guid ownerId,
            CancellationToken cancellationToken = default);

        Task<Calendar?> GetByUuIdAsync(
            Guid calendarUuId,
            CancellationToken cancellationToken = default);

    }
}
