namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;
    using System.Collections.Generic;

    public class Reminder : EntityBase
    {
        private int offsetInMinutes;
        public int OffsetInMinutes
        {
            get => offsetInMinutes;
            set
            {
                if (value > 0)
                    throw new ArgumentOutOfRangeException(
                        nameof(OffsetInMinutes), "Offset must be negative or zero.");

                offsetInMinutes = value;
            }
        }

        public ReminderMethod? Method { get; set; }

        public bool Enabled { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.UUId;
        }
    }
}
