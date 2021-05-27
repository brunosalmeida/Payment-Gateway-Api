using System;
using System.Threading.Tasks;
using PaymentGateway.Domain.Models;

namespace PaymentGateway.Model
{
    public interface IGet
    {
        Task<Payment> Get (Guid id);
    }
}