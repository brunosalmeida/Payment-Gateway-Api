namespace PaymentGateway.Dto.Request
{
    public class CreditCard
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int CVV { get; set; }
    }
}