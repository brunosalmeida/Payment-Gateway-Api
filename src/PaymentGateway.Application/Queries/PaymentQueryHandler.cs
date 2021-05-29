using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Paymentgateway.Application.Queries;
using PaymentGateway.Domain.Interfaces;
using PaymentGateway.Dto.Response;
using PaymentGateway.Infrastructure;

namespace PaymentGateway.Application.Queries
{
    public class PaymentQueryHandler : IRequestHandler<PaymentQuery, PaymentQueryResult>
    {
        private readonly IPaymentRepositoryResiliencePolicy _repository;

        public PaymentQueryHandler(IPaymentRepositoryResiliencePolicy repository)
        {
            _repository = repository;
        }

        public async Task<PaymentQueryResult> Handle(PaymentQuery query, CancellationToken cancellationToken)
        {
            var payment = await _repository.Get(query.Id);

            if (payment is null)
                return null;

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