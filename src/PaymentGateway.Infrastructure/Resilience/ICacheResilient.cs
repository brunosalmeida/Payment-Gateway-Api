using System;
using System.Threading.Tasks;
using PaymentGateway.Domain.Models;

namespace PaymentGateway.Infrastructure.Resilience
{
    public interface ICacheResilient
    {
        Task<Payment> Get(Guid id);
        Task Set(Payment payment);
    }
}