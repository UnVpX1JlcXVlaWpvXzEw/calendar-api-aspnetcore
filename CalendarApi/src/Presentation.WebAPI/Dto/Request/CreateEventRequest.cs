namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request
{
    public class CreateEventRequest
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Location { get; set; } = string.Empty;
    }
}
