namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response
{
    public class CalendarSummary
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid OwnerId { get; set; }
    }
}
