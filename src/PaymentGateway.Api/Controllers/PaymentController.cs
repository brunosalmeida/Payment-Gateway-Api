using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paymentgateway.Application.Commands;
using PaymentGateway.Dto;
using PaymentGateway.Dto.Request;
using PaymentGateway.Dto.Response;

namespace PaymentGateway.Api.Controllers
{
    [ApiController]
    [Route("v1/payment")]
    public class PaymentController : MainController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PaymentController> _logger;
        public PaymentController(ILogger<PaymentController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddPayment(Payment payment)
        {
            var command = new PaymentCommand(payment);
            var result = await _mediator.Send(command);
            
            if(result.Status == PaymentStatus.Error) 
                AddProcessingError("Payment not allowed.");
            
            return CustomResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayment()
        {
            return NotFound();
        }
    }
}