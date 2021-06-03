using System;
using System.Threading.Tasks;
using PaymentGateway.Domain.Models;
using PaymentGateway.Dto.Response;

namespace PaymentGateway.Infrastructure.Resilience
{
    public interface ICacheResilient
    {
        Task<PaymentQueryResult> Get(Guid id);
        Task Set(PaymentQueryResult payment);
    }
}