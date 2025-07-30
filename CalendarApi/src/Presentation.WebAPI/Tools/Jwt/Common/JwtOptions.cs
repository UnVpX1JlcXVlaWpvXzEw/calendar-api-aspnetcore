namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Jwt.Common
{
    public class JwtOptions
    {
        public const string SectionName = "Jwt";

        public string? SecurityKey { get; set; }

        public string? ValidIssuer { get; set; }

        public string? ValidAudience { get; set; }

        public double ExpiryInSeconds { get; set; } = 3600;
    }
}