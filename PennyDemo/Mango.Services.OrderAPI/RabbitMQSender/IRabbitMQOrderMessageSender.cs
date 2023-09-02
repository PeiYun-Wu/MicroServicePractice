using Mango.MessageBus;

namespace Mango.Services.OrderAPI.RabbitMQSender
{
    public interface IRabbitMQOrderMessageSender
    {
        void SendMessage(BaseMessage baseMessage, String queueName); //傳送給Azure的訊息內容
    }
}
