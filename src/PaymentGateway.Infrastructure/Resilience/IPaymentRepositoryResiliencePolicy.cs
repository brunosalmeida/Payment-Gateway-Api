using System;
using System.Threading.Tasks;
using PaymentGateway.Domain.Models;

namespace PaymentGateway.Infrastructure.Resilience
{
    public interface IPaymentRepositoryResiliencePolicy
    {
        Task<Payment> Get(Guid id);
        Task<int> Insert(Payment payment);
    }
}