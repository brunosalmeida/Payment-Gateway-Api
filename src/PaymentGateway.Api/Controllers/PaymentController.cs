using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Dto.Request;

namespace PaymentGateway.Api.Controllers
{
    [ApiController]
    [Route("v1/payment")]
    public class PaymentController : MainController
    {
        [HttpPost]
        public async Task<IActionResult> Payment(Payment payment)
        {
            return Created("Payment created", payment);
        }

        [HttpGet]
        public async Task<IActionResult> Payment()
        {
            return NotFound();
        }
    }
}