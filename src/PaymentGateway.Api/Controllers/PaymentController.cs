using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Api.Validadors;
using Paymentgateway.Application.Commands;
using Paymentgateway.Application.Queries;
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
            var validator = new PaymentValidator();
            var validationResult = await validator.ValidateAsync(payment, default);

            if (!validationResult.IsValid)
            {
                return CustomResponse(validationResult = validationResult);
            }

            var command = new PaymentCommand(payment);
            var result = await _mediator.Send(command);

            return CustomResponse(result);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetPayment(Guid id)
        {   
            var query = new PaymentQuery(id);
            var result = await _mediator.Send(query);

            return CustomResponse(result);
        }
    }
}