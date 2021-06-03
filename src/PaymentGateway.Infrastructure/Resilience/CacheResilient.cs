using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PaymentGateway.Dto.Response;
using PaymentGateway.Infrastructure.Cache;
using Polly;
using Polly.CircuitBreaker;
using StackExchange.Redis;

namespace PaymentGateway.Infrastructure.Resilience
{
    public class CacheResilient : ICacheResilient
    {
        private readonly ICache _cache;
        private readonly ConnectionMultiplexer _redis;
        private int _retryCount = 3;
        private readonly AsyncCircuitBreakerPolicy _circuitBreaker;
        private const string _database = "Redis:ConnectionString";

        public CacheResilient(IConfiguration configuration)
        {
            _circuitBreaker = Policy.Handle<Exception>()
                .CircuitBreakerAsync(_retryCount, TimeSpan.FromSeconds(60),
                    (ex, timespan) => { Console.WriteLine("Broken..."); },
                    () => { Console.WriteLine("Reseting..."); });

            var policy = Policy<ConnectionMultiplexer>.Handle<Exception>()
                .FallbackAsync(fallbackAction: cancelationToken => this.FallbackGetActionAsync())
                .WrapAsync(_circuitBreaker);

            _redis = policy.ExecuteAsync(async () =>
                    await StackExchange.Redis.ConnectionMultiplexer.ConnectAsync(configuration.GetSection(_database)
                        .Value))
                .Result;

            _cache = new Cache.Cache(_redis);
        }

        private async Task<ConnectionMultiplexer> FallbackGetActionAsync()
        {
            return await Task.FromResult((ConnectionMultiplexer) null);
        }

        public async Task<PaymentQueryResult> Get(Guid id)
        {
            if (_redis is null) return (PaymentQueryResult) null;

            return await _cache.Get(id);
        }

        public async Task Set(PaymentQueryResult payment)
        {
            if (_redis is not null)
            {
                await _cache.Set(payment);
            }
        }
    }
}