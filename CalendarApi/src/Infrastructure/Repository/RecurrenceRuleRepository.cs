namespace HustleAddiction.Platform.CalendarApi.Infrastructure.Repository
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;

    internal class RecurrenceRuleRepository(CalendarAPIDbContext context)
        : GenericRepository<RecurrenceRule>(context),
            IRecurrenceRuleRepository
    {
    }
}
