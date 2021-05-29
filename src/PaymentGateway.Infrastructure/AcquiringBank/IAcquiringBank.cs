using System.Threading.Tasks;
using PaymentGateway.Dto.AcquiringBankPayment;

namespace PaymentGateway.Infrastructure.AcquiringBank
{
    public interface IAcquiringBank
    {
        Task<AcquirinBankPaymentResult> Send(AcquirinBankPayment payment);
    }
}