﻿using Mango.MessageBus;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Mango.Services.PaymentAPI.RabbitMQSender
{
    public class RabbitMQPaymentMessageSender : IRabbitMQPaymentMessageSender
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;
        private const string ExchangeName = "DirectPaymentUpdate_Exchange";
        private const string PaymentEmailUpdateQueueName = "PaymentEmailUpdateQueueName";
        private const string PaymentOrderUpdateQueueName = "PaymentOrderUpdateQueueName";

        public RabbitMQPaymentMessageSender()
        {
            _hostname = "localhost";
            _password = "guest";
            _username = "guest";
        }

        public void SendMessage(BaseMessage message)
        {
            //使用fanout Exchange
            if (ConnectionExists())
            {
                //發送消息
                using var channel = _connection.CreateModel();
                channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, durable: false);
                channel.QueueDeclare(PaymentOrderUpdateQueueName, false, false, false, null);
                channel.QueueDeclare(PaymentEmailUpdateQueueName, false, false, false, null);
                channel.QueueBind(PaymentEmailUpdateQueueName, ExchangeName, "PaymentEmail");
                channel.QueueBind(PaymentOrderUpdateQueueName, ExchangeName, "PaymentOrder");

                //編碼消息
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: ExchangeName, "PaymentEmail", basicProperties: null, body: body);
                channel.BasicPublish(exchange: ExchangeName, "PaymentOrder", basicProperties: null, body: body);

            }
        }
        private void CreateConnection()
        {
            try
            {
                //創建連結
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch(Exception ex)
            {

            }

        }
        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }
            CreateConnection();
            return _connection != null;
        }
    }
}
