using System;

namespace PaymentGateway.Domain.Models
{
    public sealed class CreditCard
    {
        public string Name { get; }
        public string Number { get; private set; }
        public int Month { get; }
        public int Year { get; }
        public string CVV { get; }

        public CreditCard(string name, string number, int month, int year, string cvv)
        {
            Name = name;
            Number = number;
            Month = month;
            Year = year;
            CVV = cvv;
        }
        
        public void Mask()
        {
            var lastDigits = Number.Substring(Number.Length - 4, 4);
            Number = $"{ new String('X', Number.Length - lastDigits.Length)}{lastDigits}";
        }
    }
}