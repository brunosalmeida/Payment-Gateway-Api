using MediatR;
using PaymentGateway.Dto.Request;
using PaymentGateway.Dto.Response;

namespace Paymentgateway.Application.Commands
{
    public class PaymentCommand : IRequest<PaymentResult>
    {
        public readonly Payment Payment;
        
        public PaymentCommand(Payment payment)
        {
            Payment = payment;
        }
    }


}