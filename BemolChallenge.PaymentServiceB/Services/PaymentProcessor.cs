

using BemolChallenge.PaymentServiceB.Models;
using BemolChallenge.PaymentServiceB.Repositories;
using BemolChallenge.PaymentServiceB.Services.QuereService;
using Newtonsoft.Json;

namespace BemolChallenge.PaymentServiceB.Services
{
    public class PaymentProcessor : BackgroundService
    {
        private readonly IQueueService _queueService;
        private readonly IPaymentRepository? _payments;
        private readonly Random _random;
        private int _changeVisibilitySeconds = 5;
        public PaymentProcessor(IQueueService queueService, IPaymentRepository payments)
        {
            _queueService = queueService;
            _payments = payments;
            _random = new Random();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Starting Background processor");
            while (stoppingToken.IsCancellationRequested)
            {
                var response = await _queueService.ReceiveMessagesAsync();
                foreach (var message in response.Messages)
                {
                    try
                    {
                        if (_random.Next(0, 2) == 0)
                        {
                            throw new Exception("Simulated processing error");
                        }

                        var paymentIntent = JsonConvert.DeserializeObject<PaymentIntent>(message.Body);

                        var paymentRecord = new Payment
                        {
                            Uuid = paymentIntent.Uuid,
                            Description = paymentIntent.Description,
                            Amount = paymentIntent.Amount,
                            Currency = paymentIntent.Currency,
                            CreatedAt = paymentIntent.createdAt,
                            Status = "Success",
                            ProcessedAt = DateTime.UtcNow,
                        };

                        await _payments.CreatePayment(paymentRecord);
                        await _queueService.DeleteMessageAsync(message.ReceiptHandle);
                    }
                    catch (Exception ex)
                    {
                        await _queueService.NacknowledgeMessageAsync(message.ReceiptHandle, _changeVisibilitySeconds);
                    }
                }
            }

        }
    }
}
