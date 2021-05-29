using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Paymentgateway.Application.Queries;
using PaymentGateway.Application.Queries;
using PaymentGateway.Domain.Interfaces;
using PaymentGateway.Dto.Response;
using PaymentGateway.Infrastructure;
using Xunit;

namespace PaymentGateway.Application.Test
{
    public class PaymentQueryHandlerTest
    {
        [Fact(DisplayName = "Add new payment and returns a successful status")]
        public async Task GetPaymentShouldReturnSuccessfullStatus()
        {
            var repository = new Mock<IPaymentRepository>();
            repository.Setup(m => m.Insert(It.IsAny<Domain.Models.Payment>()))
                .Returns(Task.CompletedTask);

            var policy = new Mock<IPaymentRepositoryResiliencePolicy>();
            policy.Setup(e => e.Get(It.IsAny<Guid>()));

            var id = Guid.NewGuid();
            var query = new PaymentQuery(id);

            var handler = new PaymentQueryHandler(repository.Object, policy.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(PaymentStatus.Success, result.Status);
            repository.Verify((m => m.Get(It.IsAny<Guid>())), Times.Once);
        }
    }
}