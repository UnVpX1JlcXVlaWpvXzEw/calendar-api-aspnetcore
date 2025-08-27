namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Common;

    public class UpdateEventRequest
    {
        public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public string Location { get; set; } = default!;

        public List<NotificationReminderDto> Reminders { get; set; } = new();
    }
}
