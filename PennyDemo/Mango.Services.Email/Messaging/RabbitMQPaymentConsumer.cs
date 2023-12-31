﻿using Mango.Services.Email.Messages;
using Mango.Services.Email.Repository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Mango.Services.PaymentAPI.Messaging //?
{
    public class RabbitMQPaymentConsumer : BackgroundService  //消費支付的消息
    {
       
        private IConnection _connection;  //rabbitmq套件
        private IModel _channel;
        private const string ExchangeName = "DirectPaymentUpdate_Exchange";
        private const string PaymentEmailUpdateQueueName = "PaymentEmailUpdateQueueName";
        private readonly EmailRepository _emailRepo;
        string queueName = "";

        public RabbitMQPaymentConsumer(EmailRepository emailRepo  )
        {
            _emailRepo = emailRepo;
              var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();  //建立連結

            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);  //宣告交換
            _channel.QueueDeclare(PaymentEmailUpdateQueueName, false, false, false, null);
            _channel.QueueBind(PaymentEmailUpdateQueueName, ExchangeName,"PaymentEmail"); //綁定
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
            _channel.BasicConsume(PaymentEmailUpdateQueueName, false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(UpdatePaymentResultMessage updatePaymentResultMessage)
        {
            try
            {
                await _emailRepo.SendAndLogEmail(updatePaymentResultMessage);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //throw;
            }
        }
    }
}
