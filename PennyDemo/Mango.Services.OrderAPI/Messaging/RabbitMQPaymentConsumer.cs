using Mango.Services.OrderAPI.Messages;
using Mango.Services.OrderAPI.Repository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Mango.Services.OrderAPI.Messaging
{
    public class RabbitMQPaymentConsumer : BackgroundService  //繼承後台
    {
       
        private IConnection _connection;  //rabbitmq套件
        private IModel _channel;
        private const string ExchangeName = "DirectPaymentUpdate_Exchange";
        private const string PaymentOrderUpdateQueueName = "PaymentOrderUpdateQueueName";
        private readonly OrderRepository _orderRepository;
        string queueName = "";

        public RabbitMQPaymentConsumer(OrderRepository orderRepository  )
        {
            _orderRepository = orderRepository;
              var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();//建立連結
            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);  //宣告交換
            _channel.QueueDeclare(PaymentOrderUpdateQueueName, false, false, false, null);
            _channel.QueueBind(PaymentOrderUpdateQueueName, ExchangeName, "PaymentOrder"); //綁定
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested(); //要求取消

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                UpdatePaymentResultMessage updatePaymentResultMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(content);
                HandleMessage(updatePaymentResultMessage).GetAwaiter().GetResult();

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(queueName, false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(UpdatePaymentResultMessage updatePaymentResultMessage)
        {
            try
            {
                await _orderRepository.UpdateOrderPaymentStatus(updatePaymentResultMessage.OrderId,
                   updatePaymentResultMessage.Status);

                //await _messageBus.PublishMessage(paymentRequestMessage, orderPaymentProcessTopic);
                //await args.CompleteMessageAsync(args.Message);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //throw;
            }
        }
    }
}
