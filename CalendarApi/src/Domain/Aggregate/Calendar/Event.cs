namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public class Event : EntityBase
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateRange DateRange { get; set; }

        public string? Location { get; set; }

        public virtual List<Reminder> Reminders { get; set; } = [];

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.UUId;
        }

    }
}