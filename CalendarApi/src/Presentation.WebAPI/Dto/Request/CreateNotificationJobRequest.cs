namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;

    public class CreateNotificationJobRequest
    {
        public Guid calendarId { get; set; }

        public Guid eventId { get; set; }

        public int ReminderOffset { get; set; }

        public Channel Channel { get; set; }
    }
}
