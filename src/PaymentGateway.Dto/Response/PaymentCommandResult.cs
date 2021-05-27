using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace PaymentGateway.Dto.Response
{
    public struct PaymentResult
    {
        public Guid Id { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentStatus Status { get; set; }
    }
}