using Azure.Messaging.ServiceBus;
using Eagle.MessageBus;
using Eagle.Services.OrderAPI.messages;
using Eaglez.Services.OrderAPI.messages;
using Eaglez.Services.OrderAPI.Models;
using Eaglez.Services.OrderAPI.Repository;
using Newtonsoft.Json;
using System.Text;

namespace Eaglez.Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer: IAzureMessageBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionName;
        private readonly string checkOutMessageTopic;
        private readonly string orderUpdatePaymentProcessTopic;
        private readonly IConfiguration _config;
        private readonly IOrderRepo _orderRepo;
        private readonly IMessageBus _messageBus;
        //_config.GetValue<string>("Messaging:EnrouteOrderStatus")

        private ServiceBusProcessor checkOutProcessor;
        private ServiceBusProcessor orderUpdatePaymentStatusProcessor;
        public AzureServiceBusConsumer(IOrderRepo orderRepo, IConfiguration config, IMessageBus _messageBus)
        {
            _orderRepo = orderRepo;
            _config = config;
            this._messageBus = _messageBus;
            serviceBusConnectionString = _config.GetValue<string>("serviceBusConnectionString");
            subscriptionName = _config.GetValue<string>("CheckOutMessageTopic");
            checkOutMessageTopic = _config.GetValue<string>("Subscription");
            orderUpdatePaymentProcessTopic = _config.GetValue<string>("OrderUpdatePaymentProcessTopic");

            var serviceBusClient = new ServiceBusClient(serviceBusConnectionString);

            //the commented line works for topic and subscription
            //checkOutProcessor = serviceBusClient.CreateProcessor(checkOutMessageTopic, subscriptionName);

            //The line below is for queue
            checkOutProcessor = serviceBusClient.CreateProcessor("checkoutqueue");
            orderUpdatePaymentStatusProcessor = serviceBusClient.CreateProcessor(orderUpdatePaymentProcessTopic, subscriptionName);
        }

        public async Task Start()
        {
            checkOutProcessor.ProcessMessageAsync += OnCheckOutMessageReceived;
            checkOutProcessor.ProcessErrorAsync += ErrorHandler;
            await checkOutProcessor.StartProcessingAsync();

            orderUpdatePaymentStatusProcessor.ProcessMessageAsync += OnOrderPaymentUpdateRecieved;
            orderUpdatePaymentStatusProcessor.ProcessErrorAsync += ErrorHandler;
            await orderUpdatePaymentStatusProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
           await checkOutProcessor.StopProcessingAsync();
            await checkOutProcessor.DisposeAsync();

            await orderUpdatePaymentStatusProcessor.StopProcessingAsync();
            await orderUpdatePaymentStatusProcessor.DisposeAsync();
        }

        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        public async Task OnCheckOutMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            CheckOutHeaderDto checkOutHeaderDto =  JsonConvert.DeserializeObject<CheckOutHeaderDto>(body);
            OrderHeader orderHeader = new OrderHeader
            {
                UserId = checkOutHeaderDto.UserId,
                FirstName = checkOutHeaderDto.FirstName,
                LastName = checkOutHeaderDto.LastName,
                Email = checkOutHeaderDto.Email,
                CardNumber = checkOutHeaderDto.CardNumber,
                CouponCode = checkOutHeaderDto.CouponCode,
                CVV = checkOutHeaderDto.CVV,
                DisccountTotal = checkOutHeaderDto.DisccountTotal,
                ExpiryMonthYear = checkOutHeaderDto.ExpiryMonthYear,
                OrderTime = DateTime.Now,
                OrderTotal = checkOutHeaderDto.OrderTotal,
                Phone = checkOutHeaderDto.Phone,
                PickUpDateTime = checkOutHeaderDto.PickUpDateTime,
                OrderDetails = new List<OrderDetails>(),
            };
            foreach (var item in checkOutHeaderDto.CartDetails)
            {
                OrderDetails orderDetails = new OrderDetails
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    ProductPrice = item.Product.Price,
                    Count = item.Count,
                };
                orderHeader.CartTotalItems += item.Count;
                orderHeader.OrderDetails.Add(orderDetails);
            }

            await _orderRepo.AddOrder(orderHeader);


            PaymentRequestMessage requestMessage = new PaymentRequestMessage
            {
                Name = orderHeader.FirstName + " " + orderHeader.LastName,
                CardNumber = orderHeader.CardNumber,
                CVV = orderHeader.CVV,
                ExpiryMonthYear = orderHeader.ExpiryMonthYear,
                OrderId = orderHeader.OrderHeaderId,
                OrderTotal = orderHeader.OrderTotal,
                Email = orderHeader.Email,
            };
            try
            {
                await _messageBus.PublishMessage(requestMessage, "");
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task OnOrderPaymentUpdateRecieved(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            UpdatePaymentResultMessage updatePaymentResult = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);
            await _orderRepo.UpdateOrder(updatePaymentResult.OrderId, updatePaymentResult.Status);
            await args.CompleteMessageAsync(message);
        }
    }
}
