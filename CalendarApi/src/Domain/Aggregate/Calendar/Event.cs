namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public class Event : EntityBase
    {
        private List<Reminder> reminders = [];
        private List<RecurrenceRule> rules = [];

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public required DateRange DateRange { get; set; }

        public string? Location { get; set; }

        public virtual IReadOnlyCollection<Reminder>? Reminders => reminders.AsReadOnly();

        public virtual RecurrenceRule? Rule { get; set; }

        public void AddReminder(Reminder reminder)
        {
            ArgumentNullException.ThrowIfNull(reminder);

            reminders.Add(reminder);
        }

        public void AddRules(RecurrenceRule recurrenceRule)
        {
            ArgumentNullException.ThrowIfNull(recurrenceRule);

            rules.Add(recurrenceRule);
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