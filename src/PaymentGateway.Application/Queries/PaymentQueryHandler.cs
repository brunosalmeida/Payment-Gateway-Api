using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Paymentgateway.Application.Queries;
using PaymentGateway.Domain.Models;
using PaymentGateway.Dto.Response;
using PaymentGateway.Infrastructure.Resilience;

namespace PaymentGateway.Application.Queries
{
    public class PaymentQueryHandler : IRequestHandler<PaymentQuery, PaymentQueryResult>
    {
        private readonly IPaymentRepositoryResiliencePolicy _repository;
        private readonly ICacheResilient _cache;

        public PaymentQueryHandler(IPaymentRepositoryResiliencePolicy repository, ICacheResilient cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<PaymentQueryResult> Handle(PaymentQuery query, CancellationToken cancellationToken)
        {
            return await GetFromCache(query.Id) ?? await GetFromDatabase(query.Id);
        }

        private async Task<PaymentQueryResult> GetFromCache(Guid id)
        {
            var payment = await _cache.Get(id);

            return payment;
        }
        
        private async Task<PaymentQueryResult> GetFromDatabase(Guid id)
        {
            var payment = await _repository.Get(id);

            if (payment is null)
                return null;
            
            var result = CreateResult(payment);
            
            await _cache.Set(result);

            return result;
        }

        private PaymentQueryResult CreateResult(Payment payment)
        {
            return new PaymentQueryResult
            {
                Id = payment.Id,
                Amount = payment.Amount,
                Name = payment.CreditCard.Name,
                Number = payment.CreditCard.Number,
                Month = payment.CreditCard.Month,
                Year = payment.CreditCard.Year,
                CVV = payment?.CreditCard.CVV,
                Status = (PaymentStatus) payment.Status,
                CreatedDate = payment.CreatedDate
            };
        }
    }
}