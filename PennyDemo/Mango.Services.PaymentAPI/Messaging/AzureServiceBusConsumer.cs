using Azure.Messaging.ServiceBus;
using Mango.MessageBus;
using Mango.Services.PaymentAPI.Messages;
using Newtonsoft.Json;
using PaymentProcessor;
using System.Text;

namespace Mango.Services.PaymentAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        //使用 private readonly 可以在類別中宣告唯讀欄位，以提供對特定資料的存取和保護。
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionPayment;
        private readonly string orderPaymentProcessTopic;
        private readonly string orderupdatepaymenttopic;

        private ServiceBusProcessor orderPaymentProcessor;//讀取service bus


        private readonly IMessageBus _messageBus;
        private readonly IProcessPayment _processPayment;

        
        private readonly IConfiguration _configuration;

        public AzureServiceBusConsumer(IProcessPayment processPayment, IConfiguration configuration,IMessageBus messageBus)
        {
            
            _configuration = configuration;
            _messageBus = messageBus;
            _processPayment = processPayment;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionPayment = _configuration.GetValue<string>("payment1");
            orderPaymentProcessTopic = _configuration.GetValue<string>("OrderPaymentProcessTopic");
            orderupdatepaymenttopic = _configuration.GetValue<string>("Orderupdatepaymenttopic");

            var client = new ServiceBusClient(serviceBusConnectionString);

            orderPaymentProcessor = client.CreateProcessor(orderPaymentProcessTopic, subscriptionPayment);
        }
        public async Task Start()
        {
            orderPaymentProcessor.ProcessMessageAsync += ProcessPayments;
            orderPaymentProcessor.ProcessErrorAsync += ErrorHandler;
             await orderPaymentProcessor.StartProcessingAsync();
        }
        public async Task Stop()
        {
            
            await orderPaymentProcessor.StopProcessingAsync();
            await orderPaymentProcessor.DisposeAsync();
        }
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task ProcessPayments(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            PaymentRequestMessage paymentRequestMessage = JsonConvert.DeserializeObject<PaymentRequestMessage>(body);
            var result = _processPayment.PaymentProcessor();

            UpdatePaymentResultMessage updatePaymentResultMessage = new()  //消息推送到新主題
            {
                Status = result,
                OrderId = paymentRequestMessage.OrderId,
                Email = paymentRequestMessage.Email
            }; 

            try
            {
                await _messageBus.PublishMessage(updatePaymentResultMessage, orderupdatepaymenttopic);  //update需 :baseMessage
                await args.CompleteMessageAsync(args.Message);
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                //throw;
            }
        }
    }
}
