namespace HustleAddiction.Platform.CalendarApi.Infrastructure.Repository
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;

    public class ReminderRepository(CalendarAPIDbContext context)
        : GenericRepository<Reminder>(context),
        IReminderRepository
    {
    }
}
