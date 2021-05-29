using System;

namespace PaymentGateway.Dto.Response
{
    public class PaymentQueryResult
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string CVV { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}