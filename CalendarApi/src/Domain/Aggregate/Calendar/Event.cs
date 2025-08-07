namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public class Event : EntityBase
    {
        private readonly List<Reminder> reminderList = [];

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateRange DateRange { get; set; }

        public string? Location { get; set; }

        public IReadOnlyCollection<Reminder> Reminders => reminderList.AsReadOnly();

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.UUId;
        }

    }
}