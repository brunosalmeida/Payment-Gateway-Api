using System;
using System.Threading.Tasks;
using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Interfaces.Base
{
    public interface IGet
    {
        Task<Payment> Get (Guid id);
    }
}