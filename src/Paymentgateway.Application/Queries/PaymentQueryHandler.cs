using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentGateway.Domain.Interfaces;
using PaymentGateway.Dto.Response;

namespace Paymentgateway.Application.Queries
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

            return null;
        }
    }
}