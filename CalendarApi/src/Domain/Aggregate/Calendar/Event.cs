namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public class Event : EntityBase
    {
        private List<Reminder> reminders = [];
        private List<RecurrenceException> exceptions = [];
        private List<RecurrenceRule> rules = [];

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateRange DateRange { get; set; }

        public string? Location { get; set; }

        public virtual IReadOnlyCollection<Reminder> Reminders => reminders.AsReadOnly();

        public virtual IReadOnlyCollection<RecurrenceException> Exceptions => exceptions.AsReadOnly();

        public virtual IReadOnlyCollection<RecurrenceRule> Rules => rules.AsReadOnly();

        public void AddReminder(Reminder reminder)
        {
            ArgumentNullException.ThrowIfNull(reminder);

            reminders.Add(reminder);
        }

        public void RemoveReminder(Reminder reminder)
        {
            ArgumentNullException.ThrowIfNull(reminder);

            reminders.Remove(reminder);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.UUId;
        }
    }
}