namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public class RecurrenceRule : EntityBase
    {
        public Frequency Frequency { get; set; }

        public int Interval { get; set; }

        public List<string> ByDay { get; set; }

        public int? Count { get; set; }

        public DateTime? Until { get; set; }

        public DateTime Start { get; set; }

        public void Validate()
        {
            if (Count.HasValue && Until.HasValue)
                throw new ArgumentException("Cannot specify both count and until in a recurrence rule.");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.UUId;
        }
    }
}
