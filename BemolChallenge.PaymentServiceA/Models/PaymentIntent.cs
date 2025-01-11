using System.Linq;

namespace BemolChallenge.PaymentServiceA.Models
{
    public class PaymentIntent
    {
        private readonly string[] SuportedCurrencies = new[]
        {
            "CreditCard", "DebitCard", "R$", "US$"
        } ;
        public string? Description {  get; set; }
        public required decimal Amount { get; set; }
        public required string Currency {  get; set; }

        public string validate()
        {
            var errorMessage = "";
            var isCurrencySuported = this.SuportedCurrencies.FirstOrDefault(sc => sc.Equals(this.Currency));

            if (this.Amount < 0) { errorMessage += "Amount must be greater or equals to 0(zero)\n"; }
            if (isCurrencySuported is null) { errorMessage += $"Currency must be included at: {String.Join(", ", this.SuportedCurrencies)}."; }

            return errorMessage;
        }

    }
}
