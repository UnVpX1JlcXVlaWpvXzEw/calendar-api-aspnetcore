namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.GetEvent
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Repository;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider;

    public class GetEventOccurrences : IGetEventOccurrences
    {
        private ICalendarRepository calendarRepository;
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;
        public GetEventOccurrences(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            calendarRepository = provider.GetRequiredService<ICalendarRepository>();
            currentUserInfoProvider = provider.GetRequiredService<ICurrentUserInfoProvider>();
        }

        public async Task<List<EventSummary>> GetEventSummariesAsync(
            Guid calendarId,
            DateTime from,
            DateTime to,
            CancellationToken cancellationToken = default)
        {
            var ownerId = await currentUserInfoProvider.GetUserId(cancellationToken);

            var calendar = await calendarRepository.GetAsync(calendarId, cancellationToken);

            if (calendar.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You are not authorized to access this calendar.");

            var occurrences = new List<EventSummary>();

            foreach (var e in calendar.Events)
            {
                if (!e.Rules.Any())
                {
                    if (e.DateRange.Overlaps(new DateRange(from, to)))
                    {
                        occurrences.Add(BuildEventSummary(e, e.DateRange.Start));
                    }

                    continue;
                }

                foreach (var rule in e.Rules)
                {
                    if (!rule.Count.HasValue && !rule.Until.HasValue)
                        throw new InvalidOperationException("RecurrenceRule must define either Count or Until.");

                    var current = rule.Start;
                    var max = rule.Count ?? int.MaxValue;
                    var i = 0;

                    while (i < max && (!rule.Until.HasValue || current <= rule.Until))
                    {
                        if (current >= from && current <= to)
                        {
                            var exception = GetException(e, current);
                            if (exception?.OverrideTime is null && exception is not null)
                                continue;

                            occurrences.Add(BuildEventSummary(e, current, exception));
                        }

                        current = GetNextOccurrence(current, rule);
                        i++;
                    }
                }
            }

            return occurrences;
        }

        private static RecurrenceException? GetException(Event e, DateTime date)
        {
            return e.Exceptions.FirstOrDefault(ex => ex.OriginalDate.Date == date.Date);
        }

        private static EventSummary BuildEventSummary(Event e, DateTime date, RecurrenceException? exception = null)
        {
            return new EventSummary
            {
                EventId = e.UUId,
                Title = exception?.OverrideTitle ?? e.Title,
                Location = exception?.OverrideLocation ?? e.Location,
                OccurrenceDate = exception?.OverrideTime ?? date,
                OverrideTitle = exception?.OverrideTitle,
                OverrideLocation = exception?.OverrideLocation,
                OverrideTime = exception?.OverrideTime
            };
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

