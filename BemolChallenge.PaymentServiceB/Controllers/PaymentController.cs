using BemolChallenge.PaymentServiceB.Models;
using BemolChallenge.PaymentServiceB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BemolChallenge.PaymentServiceB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _payments;
        public PaymentController(IPaymentRepository payments) {
            _payments = payments;
        }

        [HttpGet("{uuid}", Name = "GetPaymentStatus")]
        public async Task<IActionResult> GetOne(string uuid)
        {
            var payment = await _payments.GetPayment(uuid);
            if (payment is null)
            {
                return NotFound();
            }
            return Ok(payment);
        }
    }
}
