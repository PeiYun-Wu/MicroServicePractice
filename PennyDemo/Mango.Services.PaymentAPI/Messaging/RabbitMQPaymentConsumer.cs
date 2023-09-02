using Mango.Services.PaymentAPI.Messages;
using Mango.Services.PaymentAPI.RabbitMQSender;
using Newtonsoft.Json;
using PaymentProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Mango.Services.PaymentAPI.Messaging
{
    public class RabbitMQPaymentConsumer : BackgroundService  //繼承後台
    {
       
        private IConnection _connection;  //rabbitmq套件
        private IModel _channel;
        private readonly IRabbitMQPaymentMessageSender _rabbitMQPaymentMessageSender;
        private readonly IProcessPayment _processPayment;

        public RabbitMQPaymentConsumer(IRabbitMQPaymentMessageSender rabbitMQPaymentMessageSender,IProcessPayment processPayment )
        {
            _processPayment = processPayment;
            _rabbitMQPaymentMessageSender = rabbitMQPaymentMessageSender;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orderpaymentprocesstopic", false, false, false, arguments: null); //接收order的訊息

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested(); //要求取消

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                PaymentRequestMessage paymentRequestMessage = JsonConvert.DeserializeObject<PaymentRequestMessage>(content);
                HandleMessage(paymentRequestMessage).GetAwaiter().GetResult();

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume("orderpaymentprocesstopic", false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(PaymentRequestMessage paymentRequestMessage)
        {
            //從AzureServiceBusConsumer複製過來

            var result = _processPayment.PaymentProcessor();

            UpdatePaymentResultMessage updatePaymentResultMessage = new()  //消息推送到新主題
            {
                Status = result,
                OrderId = paymentRequestMessage.OrderId,
                Email = paymentRequestMessage.Email
            };

            try
            {
                //發佈到多個列
                _rabbitMQPaymentMessageSender.SendMessage(updatePaymentResultMessage);

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
