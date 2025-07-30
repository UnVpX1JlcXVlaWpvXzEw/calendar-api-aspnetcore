namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.DateTimeProvider
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }

        DateTime Today { get; }

        DateTime UtcNow { get; }
    }
}