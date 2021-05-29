using System.Threading.Tasks;
using AcquiringBank.SDK;
using PaymentGateway.Dto.AcquiringBankPayment;
using PaymentStatus = AcquiringBank.SDK.PaymentStatus;

namespace PaymentGateway.Infrastructure.AcquiringBank
{
    public class AcquiringBank : IAcquiringBank
    {
        public async Task<AcquirinBankPaymentResult> Send(AcquirinBankPayment payment)
        {
            var acquiringBankPaymentService = new AcquiringBankPaymentService();
            var paymentResult = await acquiringBankPaymentService.Send(new Payment
            {
                Amount = payment.Amount,
                Month = payment.Month,
                Name = payment.Name,
                Number = payment.Number,
                Year = payment.Year,
                CVV = payment.CVV
            });

            return paymentResult.Status switch
            {
                PaymentStatus.Error => new AcquirinBankPaymentResult
                {
                    Id = paymentResult.Id, Status = Dto.AcquiringBankPayment.PaymentStatus.Error
                },
                _ => new AcquirinBankPaymentResult
                {
                    Id = paymentResult.Id, Status = Dto.AcquiringBankPayment.PaymentStatus.Success
                }
            };
        }
    }
}