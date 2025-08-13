namespace HustleAddiction.Platform.CalendarApi.Domain.Services.EventOccurrenceService
{
    using System.Collections.Generic;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;

    public interface IEventOccurrenceService
    {
        IEnumerable<EventDetails> Generate(
            Event calendarEvent,
            DateRange filterPeriod);
    }
}