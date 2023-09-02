using Azure.Messaging.ServiceBus;
using Mango.Services.Email.Messages;
using Mango.Services.Email.Repository;
using Newtonsoft.Json;
using System.Text;

namespace Mango.Services.Email.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        //使用 private readonly 可以在類別中宣告唯讀欄位，以提供對特定資料的存取和保護。
        private readonly string serviceBusConnectionString;
        private readonly string subscriptiontEmail;
        private readonly string orderUpdatePaymentTopic;

        private readonly EmailRepository _emailRepo;

        //讀取service bus
        private ServiceBusProcessor orderUpdatePaymentSatausProcessor;

        private readonly IConfiguration _configuration;

        public AzureServiceBusConsumer(EmailRepository emailRepo, IConfiguration configuration)
        {
            _emailRepo= emailRepo;
            _configuration = configuration;
     
            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptiontEmail = _configuration.GetValue<string>("SubscriptionName");
            orderUpdatePaymentTopic = _configuration.GetValue<string>("Orderupdatepaymenttopic");
            var client = new ServiceBusClient(serviceBusConnectionString);

          
            orderUpdatePaymentSatausProcessor = client.CreateProcessor(orderUpdatePaymentTopic, subscriptiontEmail);
        }
        public async Task Start()
        {
            orderUpdatePaymentSatausProcessor.ProcessMessageAsync += OnOrderPaymentUpdateReceived;
            orderUpdatePaymentSatausProcessor.ProcessErrorAsync += ErrorHandler;
            await orderUpdatePaymentSatausProcessor.StartProcessingAsync();
        }
        public async Task Stop()
        {
            
            await orderUpdatePaymentSatausProcessor.StopProcessingAsync();
            await orderUpdatePaymentSatausProcessor.DisposeAsync();
        }
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnOrderPaymentUpdateReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            UpdatePaymentResultMessage objMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);
            try
            {
                await _emailRepo.SendAndLogEmail(objMessage);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception e)
            {
                throw;
            }
        }
      
    }
}
