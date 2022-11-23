using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.MessageBus
{
    public class AzureServiceBusMessageBus : IMessageBus
    {
        //This is the connection string but wont go here as it show be in a key vault
        private string ConnectionString = string.Empty;
        public async Task PublishMessage(BaseMessage message, string topic)
        {
            await using var client = new ServiceBusClient(ConnectionString);
            ServiceBusSender sender = client.CreateSender(topic);
            var jsonMessage = JsonConvert.SerializeObject(message);
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            await sender.SendMessageAsync(finalMessage);
            //await senderClient.SendAsync(finalMessage);

            await client.DisposeAsync();
        }
    }
}
