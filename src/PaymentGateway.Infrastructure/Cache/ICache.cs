using System;
using System.Threading.Tasks;
using PaymentGateway.Domain.Models;
using PaymentGateway.Dto.Response;

namespace PaymentGateway.Infrastructure.Cache
{
    public interface ICache
    {
        Task<PaymentQueryResult> Get(Guid id);
        Task Set(PaymentQueryResult payment);
    }
}