using Azure.Messaging.ServiceBus;
using Eagle.Services.OrderAPI.messages;
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
        private readonly IConfiguration _config;
        private readonly IOrderRepo _orderRepo;
        //_config.GetValue<string>("Messaging:EnrouteOrderStatus")

        private ServiceBusProcessor checkOutProcessor;
        public AzureServiceBusConsumer(IOrderRepo orderRepo, IConfiguration config)
        {
            _orderRepo = orderRepo;
            _config = config;
            serviceBusConnectionString = _config.GetValue<string>("serviceBusConnectionString");
            subscriptionName = _config.GetValue<string>("CheckOutMessageTopic");
            checkOutMessageTopic = _config.GetValue<string>("Subscription");

            var serviceBusClient = new ServiceBusClient(serviceBusConnectionString);
            checkOutProcessor = serviceBusClient.CreateProcessor(checkOutMessageTopic, subscriptionName);
        }

        public async Task Start()
        {
            checkOutProcessor.ProcessMessageAsync += OnCheckOutMessageReceived;
            checkOutProcessor.ProcessErrorAsync += ErrorHandler;
            await checkOutProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
           await checkOutProcessor.StopProcessingAsync();
            await checkOutProcessor.DisposeAsync();
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
            
        }
    }
}
