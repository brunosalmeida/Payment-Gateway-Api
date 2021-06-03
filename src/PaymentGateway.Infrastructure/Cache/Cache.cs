using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PaymentGateway.Dto.Response;

namespace PaymentGateway.Infrastructure.Cache
{
    public class Cache : ICache
    {
        private readonly StackExchange.Redis.ConnectionMultiplexer _redis;
        public Cache(StackExchange.Redis.ConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<PaymentQueryResult> Get(Guid id)
        {
            var value = await _redis.GetDatabase(0).StringGetAsync(id.ToString());
            return value.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<PaymentQueryResult>(value);
        }

        public async Task Set(PaymentQueryResult payment)
        {
            var json = JsonConvert.SerializeObject(payment);
            await _redis.GetDatabase(0).StringSetAsync(
                payment.Id.ToString(),
                json,
                TimeSpan.FromSeconds(10));
        }
    }
}