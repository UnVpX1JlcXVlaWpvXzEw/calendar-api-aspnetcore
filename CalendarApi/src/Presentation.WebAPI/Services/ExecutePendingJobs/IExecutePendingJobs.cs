namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.ExecutePendingJobs
{
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Request;

    public interface IExecutePendingJobs
    {
        Task ExecuteAsync(
            ExecutePendingJobsRequest request,
            CancellationToken cancellationToken);
    }
}
