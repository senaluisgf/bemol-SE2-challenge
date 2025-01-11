using Amazon.SQS.Model;

namespace BemolChallenge.PaymentServiceB.Services.QuereService
{
    public interface IQueueService
    {
        Task<ReceiveMessageResponse> ReceiveMessagesAsync(int maxMessages = 10);
        Task DeleteMessageAsync(string receiptHandle);
        Task NacknowledgeMessageAsync(string receiptHandle, int visibilityTimeoutSeconds);
    }
}
