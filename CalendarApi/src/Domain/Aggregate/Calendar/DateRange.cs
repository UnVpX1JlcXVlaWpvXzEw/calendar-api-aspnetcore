namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;
    using System;
    using System.Collections.Generic;

    public class DateRange : ValueObject
    {
        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        public DateRange(DateTime start, DateTime end)
        {
            ValidateDates(start, end);

            this.Start = start;
            this.End = end;
        }

        private static void ValidateDates(DateTime start, DateTime end)
        {
            if (end <= start)
                throw new ArgumentException("End date must be after start date.");
        }

        public TimeSpan Duration() => End - Start;

        public bool Includes(DateTime dateTime) =>
            dateTime >= Start && dateTime <= End;

        public bool Overlaps(DateRange other) =>
            Start < other.End && End > other.Start;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Start;
            yield return End;
        }
    }
}
