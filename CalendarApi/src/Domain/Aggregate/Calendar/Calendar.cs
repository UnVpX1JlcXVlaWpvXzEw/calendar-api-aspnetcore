namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public class Calendar : EntityBase, IAggregateRoot
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public Guid OwnerId { get; set; }

        #region Relationships
        public ICollection<Event> Events { get; set; } = new List<Event>();
        #endregion

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.UUId;
        }
    }
}
