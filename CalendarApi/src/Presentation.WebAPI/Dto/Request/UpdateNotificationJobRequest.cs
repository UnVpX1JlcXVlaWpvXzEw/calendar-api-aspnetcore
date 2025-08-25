namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;

    public class UpdateNotificationJobRequest
    {
        public Guid calendarId { get; set; }

        public Guid notificationJobId { get; set; }

        public int? ReminderOffset { get; set; }

        public Channel? Channel { get; set; }

        public Status? Status { get; set; }
    }
}
