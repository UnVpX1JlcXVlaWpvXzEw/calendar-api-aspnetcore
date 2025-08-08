namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response
{
    public class EventSummary
    {
        public Guid EventId { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }

        public DateTime OccurrenceDate { get; set; }

        public DateTime? OverrideTime { get; set; }

        public string? OverrideTitle { get; set; }

        public string? OverrideLocation { get; set; }
    }
}
