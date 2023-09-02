using Azure.Messaging.ServiceBus;
using Mango.Services.OrderAPI.Messages;
using Mango.Services.OrderAPI.Models;
using Mango.Services.OrderAPI.Repository;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Mango.MessageBus;

namespace Mango.Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        //使用 private readonly 可以在類別中宣告唯讀欄位，以提供對特定資料的存取和保護。
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionCheckOut;
        private readonly string checkoutMessageTopic;
        private readonly string orderPaymentProcessTopic;
        private readonly string orderUpdatePaymentTopic;

        private readonly OrderRepository _orderRepository;
        private readonly IMessageBus _messageBus;

        //讀取service bus
        private ServiceBusProcessor checkOutProcessor;
        private ServiceBusProcessor orderUpdatePaymentSatausProcessor;

        private readonly IConfiguration _configuration;

        public AzureServiceBusConsumer(OrderRepository orderRepository, IConfiguration configuration,IMessageBus messageBus)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;
            _messageBus = messageBus;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionCheckOut = _configuration.GetValue<string>("SubscriptionName");
            checkoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");
            orderPaymentProcessTopic = _configuration.GetValue<string>("OrderPaymentProcessTopic");
            orderUpdatePaymentTopic = _configuration.GetValue<string>("Orderupdatepaymenttopic");
            var client = new ServiceBusClient(serviceBusConnectionString);

            checkOutProcessor = client.CreateProcessor(checkoutMessageTopic, subscriptionCheckOut);
            orderUpdatePaymentSatausProcessor = client.CreateProcessor(orderUpdatePaymentTopic, subscriptionCheckOut);
        }
        public async Task Start()
        {
            checkOutProcessor.ProcessMessageAsync += OnCheckOutMessageReceived;
            checkOutProcessor.ProcessErrorAsync += ErrorHandler;
             await checkOutProcessor.StartProcessingAsync();

            orderUpdatePaymentSatausProcessor.ProcessMessageAsync += OnOrderPaymentUpdateReceived;
            orderUpdatePaymentSatausProcessor.ProcessErrorAsync += ErrorHandler;
            await orderUpdatePaymentSatausProcessor.StartProcessingAsync();
        }
        public async Task Stop()
        {
            
            await checkOutProcessor.StopProcessingAsync();
            await checkOutProcessor.DisposeAsync();

            await orderUpdatePaymentSatausProcessor.StopProcessingAsync();
            await orderUpdatePaymentSatausProcessor.DisposeAsync();
        }
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnCheckOutMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            CheckoutHeaderDto checkoutHeaderDto = JsonConvert.DeserializeObject<CheckoutHeaderDto>(body);

            OrderHeader orderHeader = new()
            {
                UserId = checkoutHeaderDto.UserId,
                FirstName = checkoutHeaderDto.FirstName,
                LastName = checkoutHeaderDto.LastName,
                OrderDetails = new List<OrderDetails>(),
                CardNumber = checkoutHeaderDto.CardNumber,
                CouponCode = checkoutHeaderDto.CouponCode,
                CVV = checkoutHeaderDto.CVV,
                DiscountTotal = checkoutHeaderDto.DiscountTotal,
                Email = checkoutHeaderDto.Email,
                ExpiryMonthYear = checkoutHeaderDto.ExpiryMonthYear,
                OrderTime = DateTime.Now,
                OrderTotal = checkoutHeaderDto.OrderTotal,
                PaymentStatus = false,
                Phone = checkoutHeaderDto.Phone,
                PickupDateTime = checkoutHeaderDto.PickupDateTime
            };
            foreach (var detailList in checkoutHeaderDto.CartDetails)
            {
                OrderDetails orderDetails = new()
                {
                    ProductId = detailList.ProductId,
                    ProductName = detailList.Product.Name,
                    Price = detailList.Product.Price,
                    Count = detailList.Count
                };
                orderHeader.CartTotalItems += detailList.Count;
                orderHeader.OrderDetails.Add(orderDetails);
            }

            await _orderRepository.AddOrder(orderHeader);

            PaymentRequestMessage paymentRequestMessage = new()
            {
                Name = orderHeader.FirstName + " " + orderHeader.LastName,
                CardNumber = orderHeader.CardNumber,
                CVV = orderHeader.CVV,
                ExpiryMonthYear = orderHeader.ExpiryMonthYear,
                OrderId = orderHeader.OrderHeaderId,
                OrderTotal = orderHeader.OrderTotal,
                Email = orderHeader.Email
            };

            try
            {
                await _messageBus.PublishMessage(paymentRequestMessage, orderPaymentProcessTopic);
                await args.CompleteMessageAsync(args.Message);
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                //throw;
            }
        }
        private async Task OnOrderPaymentUpdateReceived (ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            UpdatePaymentResultMessage paymentResultMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);

            await _orderRepository.UpdateOrderPaymentStatus(paymentResultMessage.OrderId, paymentResultMessage.Status);
            await args.CompleteMessageAsync(args.Message);

        }
    }
}
