using System.Threading.Tasks;
using PaymentGateway.Domain.Models;

namespace PaymentGateway.Model
{
    public interface IInsert
    {
        Task Insert(Payment payment);
    }
}