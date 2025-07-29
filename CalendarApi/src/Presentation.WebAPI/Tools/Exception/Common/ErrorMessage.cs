namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Exception.Common
{
    public class ErrorMessage
    {
        public ErrorMessage()
        {
            this.Message = string.Empty;
        }

        public int? Code { get; set; }

        public string Message { get; set; }

        public int? Status { get; set; }
    }
}
