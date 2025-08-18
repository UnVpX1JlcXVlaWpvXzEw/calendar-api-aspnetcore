namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;

    public class AddRecurrenceRule
    {
        public Frequency Frequency { get; set; }

        public DateTime Start { get; set; }

        public int? Count { get; set; }

        public DateTime? Until { get; set; }
    }
}