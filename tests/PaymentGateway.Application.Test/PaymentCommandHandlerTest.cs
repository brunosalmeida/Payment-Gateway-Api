using System.Threading;
using System.Threading.Tasks;
using Moq;
using Paymentgateway.Application.Commands;
using PaymentGateway.Domain.Interfaces;
using PaymentGateway.Dto.Request;
using PaymentGateway.Dto.Response;
using PaymentGateway.Infrastructure;
using PaymentGateway.Infrastructure.Resilience;
using Xunit;

namespace PaymentGateway.Application.Test
{
    public class PaymentCommandHandlerTest
    {
        [Fact(DisplayName = "Add new payment and returns a successful status")]
        public async Task AddPaymentShouldReturnSuccessfullStatus()
        {
            var repository = new Mock<IPaymentRepositoryResiliencePolicy>();
            repository.Setup(m => m.Insert(It.IsAny<Domain.Models.Payment>()))
                .ReturnsAsync(1);
            
            var request = new Payment
            {
                Amount = 200,
                CreditCard = new CreditCard
                {
                    Name = "Natalie Buckley",
                    Number = "4088043836019395",
                    Month = 8,
                    Year = 2030,
                    CVV = "159"
                }
            };

            var command = new PaymentCommand(request);
            
            var handler = new PaymentCommandHandler(repository.Object);
            var result = await handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(PaymentStatus.Success, result.Status);
            repository.Verify((m => m.Insert(It.IsAny<Domain.Models.Payment>())), Times.Once);
        }
    }
}