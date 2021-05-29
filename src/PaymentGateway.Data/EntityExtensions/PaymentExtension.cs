using Payment = PaymentGateway.Data.Entity.Payment;

namespace PaymentGateway.Data.EntityExtensions
{
    public static class PaymentExtension
    {
        public static Domain.Models.Payment ToDomain(this Payment payment)
        {
            if (payment is null)
                return null;
            
            var creditcard =
                new Domain.Models.CreditCard(payment.Name, payment.Number, payment.Month, payment.Year, payment.CVV);

            return new Domain.Models.Payment(payment.Id, payment.Amount, creditcard, (Domain.Models.Status) payment.Status,
                payment.CreatedDate);
        }
    }
}