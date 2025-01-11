using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace BemolChallenge.PaymentServiceB.Services.QuereService
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

        public async Task<ReceiveMessageResponse> ReceiveMessagesAsync(int maxMessages = 10)
        {
            var request = new ReceiveMessageRequest
            {
                QueueUrl = _queueUrl,
                MaxNumberOfMessages = maxMessages,
                WaitTimeSeconds = 5
            };

            var response = await _sqsClient.ReceiveMessageAsync(request);
            return response;
        }
        public async Task DeleteMessageAsync(string receiptHandle)
        {
            var request = new DeleteMessageRequest
            {
                QueueUrl = _queueUrl,
                ReceiptHandle = receiptHandle
            };

            await _sqsClient.DeleteMessageAsync(request);
        }

        public async Task NacknowledgeMessageAsync(string receiptHandle, int visibilityTimeoutSeconds)
        {
            var request = new ChangeMessageVisibilityRequest
            {
                QueueUrl = _queueUrl,
                ReceiptHandle = receiptHandle,
                VisibilityTimeout = visibilityTimeoutSeconds
            };

            await _sqsClient.ChangeMessageVisibilityAsync(request);
        }
    }
}
