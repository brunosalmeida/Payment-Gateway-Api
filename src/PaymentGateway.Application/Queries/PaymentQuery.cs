using System;
using MediatR;
using PaymentGateway.Dto.Response;

namespace Paymentgateway.Application.Queries
{
    public class PaymentQuery : IRequest<PaymentQueryResult>
    {
        public readonly Guid Id;

        public PaymentQuery(Guid id)
        {
            Id = id;
        }
    }
}