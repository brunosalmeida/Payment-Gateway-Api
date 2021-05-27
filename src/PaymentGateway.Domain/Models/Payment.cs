using System;


namespace PaymentGateway.Domain.Models
{
    public class Payment : Base
    {
        public decimal Amount { get; }
        public CreditCard CreditCard { get; }

        public Status Status { get; private set; }

        public Payment(decimal amount, CreditCard creditCard)
            : base()
        {
            Amount = amount;
            CreditCard = creditCard;
        }

        public Payment(Guid id, decimal amount, string name, string number, int month, int year, int cvv, Status status,
            DateTime createdDate)
            : base(id, createdDate)
        {
            Amount = amount;
            CreditCard = new CreditCard(name, number, month, year, cvv);
            Status = status;
        }

        public void SuccessPayment()
        {
            Status = Status.Success;
        }

        public void ErrorPayment()
        {
            Status = Status.Error;
        }
    }
}