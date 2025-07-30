namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public class Calendar : EntityBase, IAggregateRoot
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.UUId;
        }
    }
}
