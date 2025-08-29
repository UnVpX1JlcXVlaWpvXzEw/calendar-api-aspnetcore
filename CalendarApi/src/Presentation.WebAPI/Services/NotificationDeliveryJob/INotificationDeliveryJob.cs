namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.NotificationDeliveryJob
{
    public interface INotificationDeliveryJob
    {
        Task DeliveryAsync(CancellationToken cancellationToken);
    }
}
