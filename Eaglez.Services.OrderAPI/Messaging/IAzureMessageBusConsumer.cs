namespace Eaglez.Services.OrderAPI.Messaging
{
    public interface IAzureMessageBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
