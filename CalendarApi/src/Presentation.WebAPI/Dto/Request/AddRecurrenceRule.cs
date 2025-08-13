namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;

    public class AddRecurrenceRule
    {
        public Frequency Frequency { get; set; }
        public int Interval { get; set; } = 1;
        public DateTime Start { get; set; }
        public int? Count { get; set; }
        public DateTime? Until { get; set; }
        public List<string>? ByDay { get; set; }
    }
}