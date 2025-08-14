namespace HustleAddiction.Platform.CalendarApi.Domain.Services.EventOccurrenceService
{
    using System;

    public class EventDetails
    {
        public Guid EventId { get; set; }

        public Guid? RecurrenceRuleId { get; set; }

        public DateTime Date { get; set; }

        public string? Title { get; set; }

        public string? Location { get; set; }
    }
}
