using IdentityModel;
using System.Security.Claims;

namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.ClaimsPrincipalExtensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool TryGetUsername(this ClaimsPrincipal principal, out string? username)
        {
            ArgumentNullException.ThrowIfNull(principal, nameof(principal));

            username = principal.Claims
                .FirstOrDefault(c =>
                       c.Type == JwtClaimTypes.PreferredUserName ||
                       c.Type == JwtClaimTypes.Name)
                ?.Value;

            return !string.IsNullOrWhiteSpace(username);
        }

        public static bool TryGetUserId(this ClaimsPrincipal principal, out Guid userId)
        {
            ArgumentNullException.ThrowIfNull(principal, nameof(principal));

            userId = Guid.Empty;

            var idClaim = principal.FindFirst("sub")
                       ?? principal.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim is not null && Guid.TryParse(idClaim.Value, out var parsed))
            {
                userId = parsed;
                return true;
            }

            return false;
        }

    }
}
