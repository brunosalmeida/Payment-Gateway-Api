using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Paymentgateway.Application.Queries;
using PaymentGateway.Application.Queries;
using PaymentGateway.Domain.Models;
using PaymentGateway.Dto.Response;
using PaymentGateway.Infrastructure.Cache;
using PaymentGateway.Infrastructure.Resilience;
using Xunit;

namespace PaymentGateway.Application.Test
{
    public class PaymentQueryHandlerTest
    {
        [Fact(DisplayName = "Get payment and returns a successful status")]
        public async Task GetPaymentShouldReturnSuccessfullStatus()
        {
            var id = Guid.NewGuid();
            var creditCard = new CreditCard("Natalie Buckley", "4088043836019395", 8, 2030, "159");
            var payment = new Payment(id, 200, creditCard);

            var repository = new Mock<IPaymentRepositoryResiliencePolicy>();
            repository.Setup(e => e.Get(It.IsAny<Guid>())).ReturnsAsync(payment);

            var cache = new Mock<ICacheResilient>();
            cache.Setup(e => e.Get(It.IsAny<Guid>())).ReturnsAsync(payment);
            
            var query = new PaymentQuery(id);

            var handler = new PaymentQueryHandler(repository.Object, cache.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(PaymentStatus.Success, result.Status);
            cache.Verify((m => m.Get(It.IsAny<Guid>())), Times.Once);
            repository.Verify((m => m.Get(It.IsAny<Guid>())), Times.Never);
        }
        
        [Fact(DisplayName = "Get payment and returns null")]
        public async Task GetPaymentShouldReturnNull()
        {
            var repository = new Mock<IPaymentRepositoryResiliencePolicy>();
            repository.Setup(e => e.Get(It.IsAny<Guid>())).ReturnsAsync(default(Payment));

            var cache = new Mock<ICacheResilient>();
            cache.Setup(e => e.Get(It.IsAny<Guid>())).ReturnsAsync(default(Payment));

            var id = Guid.NewGuid();
            var query = new PaymentQuery(id);

            var handler = new PaymentQueryHandler(repository.Object, cache.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result);
            cache.Verify((m => m.Get(It.IsAny<Guid>())), Times.Once);
            repository.Verify((m => m.Get(It.IsAny<Guid>())), Times.Once);
        }
    }
}