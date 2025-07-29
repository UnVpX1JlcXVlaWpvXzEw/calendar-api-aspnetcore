using System.Diagnostics.CodeAnalysis;

namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.HttpRequest
{
    public static class HttpRequestExtensions
    {
        private const string BearerPrefix = "bearer ";

        public static bool TryGetToken(
            this Microsoft.AspNetCore.Http.HttpRequest request,
            [NotNullWhen(true)] out string? token)
        {
            if (!request.Headers.TryGetValue("Authorization", out var authorizationValue))
            {
                token = null;
                return false;
            }

            token = authorizationValue.ToString();

            if (token.StartsWith(BearerPrefix, StringComparison.InvariantCultureIgnoreCase))
            {
                token = token[BearerPrefix.Length..];
            }

            return !string.IsNullOrWhiteSpace(token);
        }
    }
}