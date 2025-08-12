namespace HustleAddiction.Platform.CalendarApi.Domain.Services.EventOccurrenceService
{
    using System;

    public class EventOccurrence
    {
        public DateTime OriginalDate { get; set; }
        public DateTime? OverrideTime { get; set; }
        public string? OverrideTitle { get; set; }
        public string? OverrideLocation { get; set; }
    }
}
