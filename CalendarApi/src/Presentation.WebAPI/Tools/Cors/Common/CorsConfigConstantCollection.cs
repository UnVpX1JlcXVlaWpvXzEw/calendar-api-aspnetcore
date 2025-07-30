namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Cors.Common
{
    internal static class CorsConfigConstantCollection
    {
        private const string DefaultOriginCollectionName = "AllowSpecificOrigin";

        private const string DefaultAllowedOrigins = "*";

        private static CorsConfigConstant CorsAllowedOrigins => new("Cors:AllowedOrigins");

        private static CorsConfigConstant CorsOriginCollectionName => new("Cors:OriginCollectionName");

        internal static string[] GetCorsAllowedOrigins(IConfiguration configuration)
            => configuration.GetSection(CorsAllowedOrigins.Value).Get<string[]>()
            ?? [DefaultAllowedOrigins];

        internal static string GetCorsOriginCollectionName(IConfiguration configuration)
            => configuration.GetSection(CorsOriginCollectionName.Value).Value
            ?? DefaultOriginCollectionName;
    }
}