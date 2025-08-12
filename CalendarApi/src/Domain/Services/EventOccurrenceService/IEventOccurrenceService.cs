using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;

namespace HustleAddiction.Platform.CalendarApi.Domain.Services.EventOccurrenceService
{
    using System;
    using System.Collections.Generic;

    public interface IEventOccurrenceService
    {
        IEnumerable<EventDetails> Generate(
            Event calendarEvent,
            DateTime from,
            DateTime to);
    }
}