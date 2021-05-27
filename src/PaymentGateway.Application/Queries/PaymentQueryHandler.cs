﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Paymentgateway.Application.Queries;
using PaymentGateway.Domain.Interfaces;
using PaymentGateway.Dto.Response;

namespace PaymentGateway.Application.Queries
{
    public class PaymentQueryHandler: IRequestHandler<PaymentQuery, PayementQueryResult>
    {
        private readonly IPaymentRepository _repository;

        public PaymentQueryHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PayementQueryResult> Handle(PaymentQuery query, CancellationToken cancellationToken)
        {
            var payment = await _repository.Get(query.Id);

            return new PayementQueryResult
            {
                Id = payment.Id,
                Amount = payment.Amount,
                Name = payment.CreditCard.Name,
                Number = payment.CreditCard.Number,
                Month = payment.CreditCard.Month,
                Year = payment.CreditCard.Year,
                CVV = payment.CreditCard.CVV,
                Status = (PaymentStatus)payment.Status,
                CreatedDate = payment.CreatedDate
            };
        }
    }
}