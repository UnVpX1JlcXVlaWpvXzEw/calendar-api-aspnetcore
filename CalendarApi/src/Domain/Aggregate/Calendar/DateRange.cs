namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;
    using System;
    using System.Collections.Generic;

    public class DateRange : ValueObject
    {
        private DateTime start;
        private DateTime end;

        public DateTime Start
        {
            get => start;
            set
            {
                if (value >= End)
                    throw new ArgumentException("Start date must be before end date.");
                start = value;
            }
        }

        public DateTime End
        {
            get => end;
            set
            {
                if (value <= Start)
                    throw new ArgumentException("End date must be after start date.");
                end = value;
            }
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
