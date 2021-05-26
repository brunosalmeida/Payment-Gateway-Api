using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.Api.Controllers;
using Paymentgateway.Application;
using PaymentGateway.Dto.Request;
using Xunit;

namespace PaymentGateway.Api.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task AddNewPaymentShouldReturnOk()
        {
            var id = Guid.NewGuid();
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<PaymentCommand>(), CancellationToken.None)).ReturnsAsync(id);

            var log = new Mock<ILogger<PaymentController>>();
            var controller = new PaymentController(log.Object, mediator.Object);

            var request = new Payment
            {
                Amount = 200,
                CreditCard = new CreditCard
                {
                    Name = "Natalie Buckley",
                    Number = "4088043836019395",
                    Month = 8,
                    Year = 2030,
                    CVV = 159
                }
            };

            var result = await controller.AddPayment(request) as OkObjectResult;
            Assert.Equal(200, result?.StatusCode);
            mediator.Verify(m => m.Send(It.IsAny<PaymentCommand>(), CancellationToken.None), Times.Once);
        }
    }
}