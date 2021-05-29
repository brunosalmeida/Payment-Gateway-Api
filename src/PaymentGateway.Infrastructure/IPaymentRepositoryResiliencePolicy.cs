using System;
using System.Threading.Tasks;
using PaymentGateway.Domain.Models;

namespace PaymentGateway.Infrastructure
{
    public interface IPaymentRepositoryResiliencePolicy
    {
        Task<Payment> Get(Guid id);
    }
}