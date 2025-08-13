namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;

    public class CreateEventRequest
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateRange DateRange { get; set; } = default!;

        public string Location { get; set; } = string.Empty;

        public AddRecurrenceRule? RecurrenceRule { get; set; }
    }
}