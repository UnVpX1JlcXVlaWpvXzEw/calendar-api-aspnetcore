namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.DateTimeProvider
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        public DateTime Today
        {
            get { return DateTime.Today; }
        }

        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}