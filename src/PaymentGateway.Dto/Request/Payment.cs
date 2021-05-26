namespace PaymentGateway.Dto.Request
{
    public struct Payment
    {
        public decimal Amount { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}