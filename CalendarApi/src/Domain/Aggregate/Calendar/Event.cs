namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public class Event : EntityBase
    {
        public Guid OwnerId { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string? Location { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.UUId;
        }
    }
}