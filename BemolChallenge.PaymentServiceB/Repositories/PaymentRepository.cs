using BemolChallenge.PaymentServiceB.Database;
using BemolChallenge.PaymentServiceB.Models;
using MongoDB.Driver;

namespace BemolChallenge.PaymentServiceB.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IMongoCollection<Payment>? _payments;

        public PaymentRepository(MongoDBService mongoDBService)
        {
            _payments = mongoDBService.Database?.GetCollection<Payment>("payments");
        }

        public async Task<Payment> GetPayment(string uuid)
        {
            var payment = await _payments.FindAsync(filter: x => x.Uuid == uuid);
            if (payment is null)
            {
                return null;
            }
            return (Payment)payment;
        }
        public async Task<Payment> CreatePayment(Payment payment)
        {
            await _payments?.InsertOneAsync(payment);
            return payment;
        }
    }
}
