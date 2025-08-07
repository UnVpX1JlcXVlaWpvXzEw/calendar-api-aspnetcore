namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public class Event : EntityBase
    {
        private List<Reminder> reminders = [];

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateRange DateRange { get; set; }

        public string? Location { get; set; }

        public virtual IReadOnlyCollection<Reminder> Reminders => reminders.AsReadOnly();

        public void AddReminder(Reminder newReminder)
        {
            ArgumentNullException.ThrowIfNull(newReminder);

            reminders.Add(newReminder);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.UUId;
        }
    }
}