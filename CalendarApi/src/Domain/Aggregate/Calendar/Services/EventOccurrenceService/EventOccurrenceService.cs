namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Services.EventOccurrenceService
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;

    public class EventOccurrenceService : IEventOccurrenceService
    {
        public IEnumerable<EventOccurrence> Generate(Event calendarEvent, DateTime from, DateTime to)
        {
            ArgumentNullException.ThrowIfNull(calendarEvent);

            if (calendarEvent.DateRange is null)
                yield break;

            var interval = new DateRange(from, to);

            var rules = calendarEvent.Rules ?? Enumerable.Empty<RecurrenceRule>();
            var exceptions = calendarEvent.Exceptions ?? Enumerable.Empty<RecurrenceException>();

            if (!rules.Any())
            {
                if (calendarEvent.DateRange.Overlaps(interval))
                {
                    yield return new EventOccurrence
                    {
                        OriginalDate = calendarEvent.DateRange.Start,
                        OverrideTime = null,
                        OverrideTitle = null,
                        OverrideLocation = null
                    };
                }
                yield break;
            }

            foreach (var rule in rules)
            {
                if (!rule.Count.HasValue && !rule.Until.HasValue)
                    throw new InvalidOperationException("RecurrenceRule must define either Count or Until.");

                var current = rule.Start;
                var max = rule.Count ?? int.MaxValue;
                var i = 0;

                while (i < max && (!rule.Until.HasValue || current <= rule.Until.Value))
                {
                    if (current > to) break;

                    if (current >= from && current <= to)
                    {
                        var ex = exceptions.FirstOrDefault(x => x.OriginalDate.Date == current.Date);

                        if (ex is null || ex.OverrideTime.HasValue)
                        {
                            yield return new EventOccurrence
                            {
                                OriginalDate = current,
                                OverrideTime = ex?.OverrideTime,
                                OverrideTitle = ex?.OverrideTitle,
                                OverrideLocation = ex?.OverrideLocation
                            };
                        }

                        i++;
                    }

                    current = GetNextOccurrence(current, rule);
                }
            }
        }

        private static DateTime GetNextOccurrence(DateTime current, RecurrenceRule rule)
        {
            switch (rule.Frequency)
            {
                case Frequency.Daily:
                    return current.AddDays(rule.Interval);
                case Frequency.Weekly:
                    return current.AddDays(7 * rule.Interval);
                case Frequency.Monthly:
                    return current.AddMonths(rule.Interval);
                case Frequency.Yearly:
                    return current.AddYears(rule.Interval);
                default:
                    throw new NotSupportedException($"Unsupported frequency: {rule.Frequency}");
            }
        }
    }
}
