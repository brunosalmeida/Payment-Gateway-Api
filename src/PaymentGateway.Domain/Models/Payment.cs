using System;

namespace PaymentGateway.Domain.Models
{
    public class Payment : Base
    {
        public decimal Amount { get; }
        public CreditCard CreditCard { get; set; }

        public Payment(decimal amount, CreditCard creditCard) 
            : base()
        {
            Amount = amount;
            CreditCard = creditCard;
        }
        
        public Payment(Guid id, decimal amount, CreditCard creditCard, DateTime createdDate)
            : base(id, createdDate)
        {
            Amount = amount;
            CreditCard = creditCard;
        }
    }
}