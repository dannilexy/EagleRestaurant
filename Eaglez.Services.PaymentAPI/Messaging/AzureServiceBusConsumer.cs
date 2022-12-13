using Azure.Messaging.ServiceBus;
using Eagle.MessageBus;
using Eaglez.Services.PaymentAPI.messages;
using Newtonsoft.Json;
using PaymentProcessor;
using System.Text;

namespace Eaglez.Services.PaymentAPI.Messaging
{
    public class AzureServiceBusConsumer: IAzureMessageBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionName;
        private readonly string orderPaymentProcessTopic;
        private readonly IProcessPayment _processPayment;
        private readonly IConfiguration _config;
        private readonly IMessageBus _messageBus;
        //_config.GetValue<string>("Messaging:EnrouteOrderStatus")

        private ServiceBusProcessor OrderPaymentProcessor;
        public AzureServiceBusConsumer(IConfiguration config, IMessageBus _messageBus)
        {
            _config = config;
            this._messageBus = _messageBus;
            serviceBusConnectionString = _config.GetValue<string>("serviceBusConnectionString");
            subscriptionName = _config.GetValue<string>("OrderPaymentProcessSubscription");
            orderPaymentProcessTopic = _config.GetValue<string>("OrderProcessTopics");

            var serviceBusClient = new ServiceBusClient(serviceBusConnectionString);
            OrderPaymentProcessor = serviceBusClient.CreateProcessor(orderPaymentProcessTopic, subscriptionName);
        }

        public async Task Start()
        {
            OrderPaymentProcessor.ProcessMessageAsync += ProcessPayments;
            OrderPaymentProcessor.ProcessErrorAsync += ErrorHandler;
            await OrderPaymentProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
           await OrderPaymentProcessor.StopProcessingAsync();
            await OrderPaymentProcessor.DisposeAsync();
        }

        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        public async Task ProcessPayments(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            PaymentRequestMessage paymentRequestMessage =  JsonConvert.DeserializeObject<PaymentRequestMessage>(body);

            var result = await _processPayment.PaymentProcessor();

            UpdatePaymentResultMessage updatePaymentResult = new UpdatePaymentResultMessage
            {
                Status = result,
                OrderId = paymentRequestMessage.OrderId,
            };


            try
            {
                await _messageBus.PublishMessage(paymentRequestMessage, "");
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
