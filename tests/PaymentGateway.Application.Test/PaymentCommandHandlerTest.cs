using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Paymentgateway.Application.Commands;
using PaymentGateway.Application.Commands;
using PaymentGateway.Dto.AcquiringBankPayment;
using PaymentGateway.Dto.Request;
using PaymentGateway.Infrastructure.AcquiringBank;
using PaymentGateway.Infrastructure.Resilience;
using Xunit;
using PaymentStatus = PaymentGateway.Dto.Response.PaymentStatus;

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

            var id = Guid.NewGuid();
            var acquirinBankPaymentResult = new AcquirinBankPaymentResult
            {
                Id = id,
                Status = Dto.AcquiringBankPayment.PaymentStatus.Success
            };
            
            var acquiringBank = new Mock<IAcquiringBank>();
            acquiringBank.Setup(m => m.Send(It.IsAny<AcquirinBankPayment>()))
                .ReturnsAsync(acquirinBankPaymentResult);
            
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
            
            var handler = new PaymentCommandHandler(repository.Object, acquiringBank.Object);
            var result = await handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(PaymentStatus.Success, result.Status);
            repository.Verify((m => m.Insert(It.IsAny<Domain.Models.Payment>())), Times.Once);
        }
    }
}