namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Common
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;

    public class NotificationReminderDto
    {
        public int OffsetInMinutes { get; set; }

        public int? Method { get; set; }

        public bool Enabled { get; set; }

        public Channel Channel { get; set; }
    }
}
