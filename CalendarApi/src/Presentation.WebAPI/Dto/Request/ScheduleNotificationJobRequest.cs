namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;

    public class ScheduleNotificationJobRequest
    {
        public Guid CalendarId { get; set; }

        public Guid EventId { get; set; }

        public int ReminderOffset { get; set; }

        public Channel Channel { get; set; }
    }
}
