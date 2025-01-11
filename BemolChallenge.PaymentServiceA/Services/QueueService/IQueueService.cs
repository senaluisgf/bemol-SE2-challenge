namespace BemolChallenge.PaymentServiceA.Services.QueueService
{
    public interface IQueueService
    {
        Task SendMessageAsync<T>(T message);
    }
}
