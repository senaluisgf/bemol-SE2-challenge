namespace BemolChallenge.PaymentServiceB.Models
{
    public class PaymentIntent
    {
        public required string Uuid { get; set; }
        public string? Description { get; set; }
        public required decimal Amount { get; set; }
        public required string Currency { get; set; }
        public DateTime createdAt { get; set; }
    }
}
