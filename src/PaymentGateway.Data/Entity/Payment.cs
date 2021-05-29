using System;

namespace PaymentGateway.Data.Entity
{
    public class Payment
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string CVV { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }

        // public Payment(Guid id, decimal amount, string name, string number, int month, int year, string cvv,
        //     int status, DateTime createdDate)
        // {
        //     Id = id;
        //     Amount = amount;
        //     Name = name;
        //     Number = number;
        //     Month = month;
        //     Year = year;
        //     CVV = cvv;
        //     Status = status;
        //     CreatedDate = createdDate;
        // }
    }
}