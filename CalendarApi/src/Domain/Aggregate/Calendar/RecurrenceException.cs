namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;

    public class RecurrenceException : EntityBase
    {
        public DateTime OriginalDate { get; set; }

        public string? OverrideTitle { get; set; }

        public DateTime? OverrideTime { get; set; }

        public string? OverrideLocation { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.UUId;
        }
    }
}
