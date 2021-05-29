using System;

namespace PaymentGateway.Dto.AcquiringBankPayment
{
    public class AcquirinBankPaymentResult
    {
        public Guid Id { get; set; }
        public PaymentStatus Status { get; set; }
    }
}