namespace OtherServices.Services.Interfaces
{
    public interface IOrderNotificationService
    {
        Task NotifyNewOrderAsync(object orderData);
        Task NotifyOrderUpdatedAsync(object orderData);
    }

}
