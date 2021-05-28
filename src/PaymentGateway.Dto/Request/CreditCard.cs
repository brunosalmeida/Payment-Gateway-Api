namespace PaymentGateway.Dto.Request
{
    public struct CreditCard
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string CVV { get; set; }
    }
}