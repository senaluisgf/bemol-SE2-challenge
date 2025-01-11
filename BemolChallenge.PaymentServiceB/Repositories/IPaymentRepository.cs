using BemolChallenge.PaymentServiceB.Models;

namespace BemolChallenge.PaymentServiceB.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPayment(string uuid);
        Task<Payment> CreatePayment(Payment payment);
    }
}
