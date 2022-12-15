namespace Eaglez.Services.Emails.Messaging
{
    public interface IAzureMessageBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
