using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace Mango.MessageBus
{
    public class AzureServiceBusMessageBus : IMessageBus
    {
        //private string connectionString = "Endpoint=sb://mangorestaurantpenny.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=hhLb0kuB2ekaRtfKsn9PDZ5hW6VAIafTn+ASbG/htLU=";
        private string connectionString = "Endpoint=sb://testpenny.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=vD9B9DksV/rzAUKyMC9yLLXsz+aMVVWp3+ASbJlRb+s=";

        public async Task PublishMessage(BaseMessage message, string topicName)
        {
            await using var client = new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(topicName);

            var jsonMessage = JsonConvert.SerializeObject(message);
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };
           // var finalMessage = new ServiceBusMessage("test0608");
            #region old code

            //await sender.SendMessageAsync(finalMessage);

            //await sender.CloseAsync();
            #endregion
            await sender.SendMessageAsync(finalMessage);
            await client.DisposeAsync();
           
        }
    }
}
