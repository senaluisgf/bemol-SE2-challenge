using System.Text.Json;
using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace BemolChallenge.PaymentServiceA.Services.QueueService
{
    public class QueueService : IQueueService
    {
        private readonly AmazonSQSClient _sqsClient;
        private readonly string _queueUrl;
        public QueueService(IConfiguration configuration)
        {
            _queueUrl = configuration["AWS:QueueUrl"];
            var credentials = new BasicAWSCredentials(configuration["AWS:AccessKey"], configuration["AWS:SecretKey"]);
            _sqsClient = new AmazonSQSClient(credentials, RegionEndpoint.USEast1);
        }

        public async Task SendMessageAsync<T>(T message)
        {
            var messageBody = JsonSerializer.Serialize(message);
            var request = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = messageBody
            };

            await _sqsClient.SendMessageAsync(request);
        }
    }
}
