namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.ExecutePendingJobs
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;

    public interface IExecutePendingJobs
    {
        Task ExecutePendingJobsAsync(
            CalendarJobIdentifiersDto request,
            CancellationToken cancellationToken);
    }
}
