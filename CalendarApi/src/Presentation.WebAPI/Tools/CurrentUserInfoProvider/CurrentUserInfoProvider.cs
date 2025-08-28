using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.ClaimsPrincipalExtensions;

namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider
{
    public class CurrentUserInfoProvider : ICurrentUserInfoProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        private string? username;

        public CurrentUserInfoProvider(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
        }

        public Task<Guid> GetUserId(CancellationToken cancellationToken = default)
        {
            //return Task.FromResult(Guid.Parse("08ddbfc8-56c3-48b0-873a-e8b0d52f997e"));

            var user = httpContextAccessor.HttpContext?.User;

            if (user == null || !user.TryGetUserId(out var userId))
            {
                throw new InvalidOperationException("Failed to get current user from HTTP context.");
            }

            return Task.FromResult(userId);
        }

        public Task<string> GetUsername()
        {
            if (!string.IsNullOrWhiteSpace(username))
                return Task.FromResult(username!);

            var user = httpContextAccessor.HttpContext?.User;

            if (user == null || !user.TryGetUsername(out var name))
            {
                throw new InvalidOperationException("Failed to get current username from HTTP context.");
            }

            return Task.FromResult(name!);
        }
    }
}
