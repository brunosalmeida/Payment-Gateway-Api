using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaymentGateway.Domain.Models;

namespace PaymentGateway.Infrastructure.Cache
{
    public class Cache : ICache
    {
        private readonly StackExchange.Redis.ConnectionMultiplexer _redis;
        private const string _database = "Redis:ConnectionString";

        public Cache(IConfiguration configuration)
        {
            _redis = StackExchange.Redis.ConnectionMultiplexer.Connect(configuration.GetSection(_database).Value);
        }

        public async Task<Payment> Get(Guid id)
        {
            var value = await _redis.GetDatabase(0).StringGetAsync(id.ToString());
            return value.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<Payment>(value);
        }

        public async Task Set(Payment payment)
        {
            var json = JsonConvert.SerializeObject(payment);
            await _redis.GetDatabase(0).StringSetAsync(
                payment.Id.ToString(),
                json,
                TimeSpan.FromSeconds(10));
        }
    }
}