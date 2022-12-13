namespace Eaglez.Services.PaymentAPI.Messaging
{
    public interface IAzureMessageBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
