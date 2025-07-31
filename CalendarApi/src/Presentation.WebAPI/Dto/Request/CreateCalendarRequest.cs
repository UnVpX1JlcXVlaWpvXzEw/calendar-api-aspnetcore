namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCalendarRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
