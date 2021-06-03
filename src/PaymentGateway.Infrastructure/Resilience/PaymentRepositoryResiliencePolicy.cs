using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using PaymentGateway.Domain.Interfaces;
using PaymentGateway.Domain.Models;
using Polly;
using Polly.CircuitBreaker;

namespace PaymentGateway.Infrastructure.Resilience
{
    public class PaymentRepositoryResiliencePolicy : IPaymentRepositoryResiliencePolicy
    {
        private readonly IPaymentRepository _repository;
        private readonly int _retryCount = 3;
        private AsyncCircuitBreakerPolicy _circuitBreaker;

        public PaymentRepositoryResiliencePolicy(IPaymentRepository repository)
        {
            _repository = repository;

            _circuitBreaker = Policy.Handle<SqlException>()
                .CircuitBreakerAsync(_retryCount, TimeSpan.FromSeconds(60),
                    (ex, timespan) =>
                    {
                        Console.WriteLine("Broken...");
                    },
                    () => { Console.WriteLine("Reseting..."); });
        }

        public async Task<Payment> Get(Guid id)
        {
            var policy = Policy<Payment>.Handle<Exception>()
                .FallbackAsync(fallbackAction: cancelationToken => this.FallbackGetActionAsync())
                .WrapAsync(_circuitBreaker);

            return await policy.ExecuteAsync(async () => await _repository.Get(id));
        }

        public async Task<int> Insert(Payment payment)
        {
            var policy = Policy<int>.Handle<Exception>()
                .FallbackAsync(fallbackAction: cancelationToken => this.FallbackInsertActionAsync())
                .WrapAsync(_circuitBreaker);

            return await policy.ExecuteAsync(async () => await _repository.Insert(payment));
        }
        
        private async Task<Payment> FallbackGetActionAsync()
        {
            return await Task.FromResult((Payment)null);
        }
        private Task<int> FallbackInsertActionAsync()
        {
            return Task.FromResult(0);
        }

    }
}