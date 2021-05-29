namespace PaymentGateway.Dto.AcquiringBankPayment
{
    public class AcquirinBankPayment
    {
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string CVV { get; set; }

        public AcquirinBankPayment(decimal amount, string name, string number, int month, int year, string cvv)
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