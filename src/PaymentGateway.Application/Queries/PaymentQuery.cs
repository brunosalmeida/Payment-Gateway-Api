using System;
using MediatR;
using PaymentGateway.Dto.Response;

namespace Paymentgateway.Application.Queries
{
    public class PaymentQuery : IRequest<PayementQueryResult>
    {
        public readonly Guid Id;
        
        public PaymentQuery(Guid id)
        {
            Id = id;
        }
    }
}