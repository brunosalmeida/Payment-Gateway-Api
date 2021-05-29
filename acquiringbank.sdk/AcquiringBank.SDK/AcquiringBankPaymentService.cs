using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

namespace AcquiringBank.SDK
{
    public class AcquiringBankPaymentService
    {
        public async Task<PaymentResult> Send(Payment payment)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            
            return new PaymentResult
            {
                Id = Guid.NewGuid(),
                Status = DateTime.UtcNow.Ticks % 2 == 0 ? PaymentStatus.Error : PaymentStatus.Success
            };
        }
    }

    public class Payment
    {
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string CVV { get; set; }
    }

    public class PaymentResult
    {
        public Guid Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentStatus Status { get; set; }
    }

    public enum PaymentStatus
    {
        Success,
        Error
    }
}