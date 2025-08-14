namespace HustleAddiction.Platform.CalendarApi.Domain.Services.EventOccurrenceService
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;

    public class EventOccurrenceService : IEventOccurrenceService
    {
        public IEnumerable<EventDetails> Generate(
            Event calendarEvent,
            DateRange filterPeriod)
        {
            List<EventDetails> results = [];

            var rule = calendarEvent.Rule!;

            if (!rule.IsValid())
                throw new InvalidOperationException("RecurrenceRule is not valid.");

            var startDate = rule.Start;
            var untilDate = GetRuleUntilDate(rule);

            if ((startDate < filterPeriod.Start && untilDate < filterPeriod.Start)
                || (startDate > filterPeriod.End && untilDate > filterPeriod.End))
                return results;

            do
            {
                var newEvent = new EventDetails
                {
                    EventId = calendarEvent.UUId,
                    RecurrenceRuleId = rule.UUId,
                    Location = calendarEvent.Location,
                    Title = calendarEvent.Title,
                    Date = startDate
                };

                results.Add(newEvent);

                startDate = GetNextOccurrence(startDate, rule.Frequency);

            } while (startDate < filterPeriod.End);

            return results;
        }

        private static DateTime GetRuleUntilDate(RecurrenceRule rule)
        {
            if (rule.Until.HasValue)
                return rule.Until.Value;

            return rule.Frequency switch
            {
                Frequency.Daily => rule.Start.AddDays(rule.Count!.Value),
                Frequency.Weekly => rule.Start.AddDays(7 * rule.Count!.Value),
                Frequency.Monthly => rule.Start.AddMonths(rule.Count!.Value),
                Frequency.Yearly => rule.Start.AddYears(rule.Count!.Value),
                _ => throw new NotSupportedException($"Unsupported frequency: {rule.Frequency}"),
            };
        }

        private static DateTime GetNextOccurrence(DateTime current, Frequency frequency)
        {
            return frequency switch
            {
                Frequency.Daily => current.AddDays(1),
                Frequency.Weekly => current.AddDays(7),
                Frequency.Monthly => current.AddMonths(1),
                Frequency.Yearly => current.AddYears(1),
                _ => throw new NotSupportedException($"Unsupported frequency: {frequency}"),
            };
        }
    }
}
