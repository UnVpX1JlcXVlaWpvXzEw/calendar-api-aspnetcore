using IdentityModel;
using System.Security.Claims;

namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.ClaimsPrincipalExtensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool TryGetUsername(this ClaimsPrincipal principal, out string? username)
        {
            username = principal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Name)?.Value;
            return !string.IsNullOrWhiteSpace(username);
        }

        public static bool TryGetUserId(this ClaimsPrincipal principal, out Guid userId)
        {
            ArgumentNullException.ThrowIfNull(principal, nameof(principal));

            userId = Guid.Empty;

            var idClaim = principal.Claims.FirstOrDefault(
                c => c.Type == JwtClaimTypes.Id);

            if (idClaim is not null)
            {
                userId = Guid.Parse(idClaim.Value);
                return true;
            }

            return false;
        }
    }
}
