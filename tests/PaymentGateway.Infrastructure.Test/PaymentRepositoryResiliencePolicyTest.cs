using System;
using System.Threading.Tasks;
using Moq;
using PaymentGateway.Domain.Interfaces;
using PaymentGateway.Domain.Models;
using PaymentGateway.Infrastructure.Resilience;
using Xunit;

namespace PaymentGateway.Infrastructure.Test
{
    public class PaymentRepositoryResiliencePolicyTest
    {
        [Fact(DisplayName = "Add new payment and returns a successful status")]
        public async Task AddPaymentShouldReturnSuccessfullStatus()
        {
            var repository = new Mock<IPaymentRepository>();
            repository.Setup(m => m.Insert(It.IsAny<Domain.Models.Payment>()))
                .ReturnsAsync(1);

            var creditcard = new CreditCard("Natalie Buckley", "4088043836019395", 8, 2030, "159");
            var payment = new Payment(200, creditcard);

            var repositoryResiliencePolicy = new PaymentRepositoryResiliencePolicy(repository.Object);
            var result = await repositoryResiliencePolicy.Insert(payment);

            Assert.Equal(1, result);
            repository.Verify((m => m.Insert(It.IsAny<Domain.Models.Payment>())), Times.Once);
        }
        
        [Fact(DisplayName = "Get payment and returns a successful status")]
        public async Task GetPaymentShouldReturnSuccessfullStatus()
        {
            var creditCard = new CreditCard("Natalie Buckley", "4088043836019395", 8, 2030, "159");
            var payment = new Payment(200, creditCard);

            var repository = new Mock<IPaymentRepository>();
            repository.Setup(e => e.Get(It.IsAny<Guid>())).ReturnsAsync(payment);

            var id = Guid.NewGuid();
          
            var repositoryResiliencePolicy = new PaymentRepositoryResiliencePolicy(repository.Object);
            var result = await repositoryResiliencePolicy.Get(id);

            Assert.Equal(payment, result);
            repository.Verify((m => m.Get(It.IsAny<Guid>())), Times.Once);
        }
        
        [Fact(DisplayName = "Get payment and returns null")]
        public async Task GetPaymentShouldReturnNull()
        {
            var repository = new Mock<IPaymentRepository>();
            repository.Setup(e => e.Get(It.IsAny<Guid>())).ReturnsAsync(default(Payment));

            var id = Guid.NewGuid();
          
            var repositoryResiliencePolicy = new PaymentRepositoryResiliencePolicy(repository.Object);
            var result = await repositoryResiliencePolicy.Get(id);

            Assert.Null(result);
            repository.Verify((m => m.Get(It.IsAny<Guid>())), Times.Once);
        }
    }
}