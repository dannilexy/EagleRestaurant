using Azure.Messaging.ServiceBus;
using Eaglez.Services.Emails.Repository;
using Eaglez.Services.OrderAPI.messages;
using Newtonsoft.Json;
using System.Text;

namespace Eaglez.Services.Emails.Messaging
{
    public class AzureServiceBusConsumer: IAzureMessageBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionName;
        private readonly string topicName;

        private readonly IConfiguration _config;
        private readonly IEmailRepository _emailRepo;
        //private readonly IMessageBus _messageBus;
        //_config.GetValue<string>("Messaging:EnrouteOrderStatus")

        private ServiceBusProcessor orderUpdatePaymentStatusProcessor;
        public AzureServiceBusConsumer(IEmailRepository _emailRepo, IConfiguration config)
        {
            this._emailRepo = _emailRepo;
            _config = config;
            serviceBusConnectionString = _config.GetValue<string>("serviceBusConnectionString");
            subscriptionName = _config.GetValue<string>("SubscriptionName");
            topicName = _config.GetValue<string>("OrderUpdatePaymentProcessTopic");

            var serviceBusClient = new ServiceBusClient(serviceBusConnectionString);

            orderUpdatePaymentStatusProcessor = serviceBusClient.CreateProcessor(topicName, subscriptionName);
        }

        public async Task Start()
        {
           
            orderUpdatePaymentStatusProcessor.ProcessMessageAsync += OnOrderPaymentUpdateRecieved;
            orderUpdatePaymentStatusProcessor.ProcessErrorAsync += ErrorHandler;
            await orderUpdatePaymentStatusProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await orderUpdatePaymentStatusProcessor.StopProcessingAsync();
            await orderUpdatePaymentStatusProcessor.DisposeAsync();
        }

        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
       
        private async Task OnOrderPaymentUpdateRecieved(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            UpdatePaymentResultMessage paymentResultMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);



            try
            {
                await _emailRepo.SendAndLogEmail(paymentResultMessage);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
