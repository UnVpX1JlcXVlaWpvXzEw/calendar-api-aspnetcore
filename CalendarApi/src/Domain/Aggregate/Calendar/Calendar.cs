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
            if (calendarEvent is null)
            {
                throw new ArgumentNullException(nameof(calendarEvent), "The event cannot be null.");
            }

            this.events.Add(calendarEvent);
        }
    }
}
