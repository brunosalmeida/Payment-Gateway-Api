namespace PaymentGateway.Domain.Models
{
    public sealed class CreditCard
    {
        public string Name { get; }
        public string Number { get; }
        public int Month { get; }
        public int Year { get; }
        public int CVV { get; }

        public CreditCard(string name, string number, int month, int year, int cvv)
        {
            Name = name;
            Number = number;
            Month = month;
            Year = year;
            CVV = cvv;
        }
    }
}