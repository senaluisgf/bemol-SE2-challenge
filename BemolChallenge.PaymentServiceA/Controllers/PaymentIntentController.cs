
using BemolChallenge.PaymentServiceA.Models;
using BemolChallenge.PaymentServiceA.Services.QueueService;
using Microsoft.AspNetCore.Mvc;

namespace BemolChallenge.PaymentServiceA.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentIntentController : ControllerBase
    {
        private static readonly PaymentIntent[] Intents = [];
        private readonly ILogger<PaymentIntentController> _logger;
        private readonly IQueueService _queueService;

        public PaymentIntentController(ILogger<PaymentIntentController> logger, IQueueService queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }

        [HttpPost(Name = "CreatePaymentIntent")]
        public async Task<IActionResult> Post([FromBody] PaymentIntent paymentIntent)
        {
            if (paymentIntent is null)
            {
                throw new ArgumentNullException(nameof(paymentIntent));
            }

            if (paymentIntent.validate() != "")
            {
                throw new ArgumentException(paymentIntent.validate());
            }

            var uuid = Guid.NewGuid();
            var intentMessage = new
            {
                Uuid = uuid,
                paymentIntent.Amount,
                paymentIntent.Description,
                paymentIntent.Currency,
                CreatedAt = DateTime.UtcNow,
            };
            
            // Enviar a mensagem para a fila (Amazon SQS)
            await _queueService.SendMessageAsync(intentMessage);

            return Ok(new { Uuid = uuid });
        }
    }
}
