using System;

namespace PaymentGateway.Model
{
    public class Payment : Base
    {
        public decimal Amount { get; }
        public string Name { get; }
        public string Number { get; }
        public int Month { get; }
        public int Year { get; }
        public int CVV { get; }

        public Payment(decimal amount, string name, string number, int month, int year, int cvv) 
            : base()
        {
            Amount = amount;
            Name = name;
            Number = number;
            Month = month;
            Year = year;
            CVV = cvv;
        }
        
        public Payment(Guid id, decimal amount, string name, string number, int month, int year, int cvv, DateTime createdDate)
            : base(id, createdDate)
        {
            Amount = amount;
            Name = name;
            Number = number;
            Month = month;
            Year = year;
            CVV = cvv;
        }
    }
}