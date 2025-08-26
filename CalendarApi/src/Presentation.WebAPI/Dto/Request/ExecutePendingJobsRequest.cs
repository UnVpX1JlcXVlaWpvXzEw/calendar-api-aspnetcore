namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request
{
    public class ExecutePendingJobsRequest
    {
        public Guid CalendarId { get; set; }

        public Guid NotificationJobId { get; set; }
    }
}
