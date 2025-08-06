namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Common
{
    public class ReminderDto
    {
        public int OffsetInMinutes { get; set; }
        public int? Method { get; set; }
        public bool Enabled { get; set; }
    }

}
