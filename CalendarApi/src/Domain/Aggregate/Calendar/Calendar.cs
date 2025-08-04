namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public class Calendar : EntityBase, IAggregateRoot
    {
        private readonly List<Event> events = [];

        public string Name { get; set; } = string.Empty;

        public Guid OwnerId { get; set; }

        public virtual IReadOnlyCollection<Event> Events => this.events;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.UUId;
        }

        public void AddEvent(Event calendarEvent)
        {
            ArgumentNullException.ThrowIfNull(calendarEvent, nameof(calendarEvent));

            this.events.Add(calendarEvent);
        }
    }
}
