namespace HustleAddiction.Platform.CalendarApi.Domain.Services.EventOccurrenceService
{
    using System;

    public class EventDetails
    {
        public DateTime Date { get; set; }

        public DateTime? OverrideTime { get; set; }

        public string? Title { get; set; }

        public string? Location { get; set; }
    }
}
