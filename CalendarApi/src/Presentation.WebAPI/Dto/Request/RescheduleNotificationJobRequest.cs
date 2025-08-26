namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request
{
    public class RescheduleNotificationJobRequest
    {
        public Guid CalendarId { get; set; }

        public Guid EventId { get; set; }

        public Guid NotificationJobId { get; set; }

        public int? ReminderOffset { get; set; }
    }
}
