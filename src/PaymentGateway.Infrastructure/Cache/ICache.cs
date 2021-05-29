using System;
using System.Threading.Tasks;
using PaymentGateway.Domain.Models;

namespace PaymentGateway.Infrastructure.Cache
{
    public interface ICache
    {
        Task<Payment> Get(Guid id);
        Task Set(Payment payment);
    }
}