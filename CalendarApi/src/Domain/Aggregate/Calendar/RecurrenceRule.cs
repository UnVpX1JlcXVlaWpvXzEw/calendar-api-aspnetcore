namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public class RecurrenceRule : EntityBase
    {
        public Frequency Frequency { get; set; }

        public int? Count { get; set; }

        public DateTime? Until { get; set; }

        public DateTime Start { get; set; }

        public void Validate()
        {
            if (Count.HasValue && Until.HasValue)
                throw new ArgumentException("Cannot specify both count and until in a recurrence rule.");

            if (!Count.HasValue && !Until.HasValue)
                throw new ArgumentException("Either count or until must be specified in a recurrence rule.");

            if (Until.HasValue && Start >= Until.Value)
                throw new ArgumentException("The start and until of a recurrence rule can't be in the same day.");
        }

        public bool IsValid() => (Count.HasValue && !Until.HasValue) || (!Count.HasValue && Until.HasValue && Start < Until.Value);

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.UUId;
        }
    }
}
