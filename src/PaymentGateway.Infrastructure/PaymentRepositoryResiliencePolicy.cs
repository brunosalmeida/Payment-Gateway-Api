using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using PaymentGateway.Domain.Interfaces;
using PaymentGateway.Domain.Models;
using Polly;

namespace PaymentGateway.Infrastructure
{
    public class PaymentRepositoryResiliencePolicy : IPaymentRepositoryResiliencePolicy
    {
        private readonly IPaymentRepository _repository;
        private readonly int _retryCount = 3;

        public PaymentRepositoryResiliencePolicy(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Payment> Get(Guid id)
        {
            var policy = Policy<Payment>.Handle<SqlException>()
                .RetryAsync(_retryCount,
                    onRetry: (exception, retryCount) =>
                    {
                        Debug.WriteLine($"Try number {retryCount} - Exception: {exception}");
                    });

            return await policy.ExecuteAsync(async () => await _repository.Get(id));
        }

        public async Task<int> Insert(Payment payment)
        {
            var policy = Policy<int>.Handle<SqlException>()
                .RetryAsync(_retryCount,
                    onRetry: (exception, retryCount) =>
                    {
                        Debug.WriteLine($"Try number {retryCount} - Exception: {exception}");
                    });

            var result = await policy.ExecuteAsync(async () => await _repository.Insert(payment));
            return result;
        }
    }
}