namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request
{
    public class CancelNotificationJobRequest
    {
        public Guid CalendarId { get; set; }

        public Guid NotificationJobId { get; set; }
    }
}