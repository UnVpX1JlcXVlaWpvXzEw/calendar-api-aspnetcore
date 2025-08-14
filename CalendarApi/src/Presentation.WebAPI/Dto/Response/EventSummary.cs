namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response
{
    public class EventSummary
    {
        public Guid EventId { get; set; }

        public Guid? RecurrenceRuleId { get; set; }

        public DateTime Date { get; set; }

        public string? Title { get; set; }

        public string? Location { get; set; }
    }
}
