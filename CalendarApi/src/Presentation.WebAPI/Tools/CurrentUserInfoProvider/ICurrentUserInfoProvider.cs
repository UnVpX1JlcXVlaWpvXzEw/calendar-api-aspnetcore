namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.CurrentUserInfoProvider
{
    public interface ICurrentUserInfoProvider
    {
        Task<Guid> GetUserId(CancellationToken cancellationToken = default);
        string GetUsername();
    }
}