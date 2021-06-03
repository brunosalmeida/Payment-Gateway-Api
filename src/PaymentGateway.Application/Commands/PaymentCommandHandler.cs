using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Paymentgateway.Application.Commands;
using PaymentGateway.Dto.AcquiringBankPayment;
using PaymentGateway.Dto.Response;
using PaymentGateway.Infrastructure.AcquiringBank;
using PaymentGateway.Infrastructure.Resilience;
using Domain = PaymentGateway.Domain.Models;
using PaymentStatus = PaymentGateway.Dto.Response.PaymentStatus;

namespace PaymentGateway.Application.Commands
{
    public class PaymentCommandHandler : IRequestHandler<PaymentCommand, PaymentResult>
    {
        private readonly IPaymentRepositoryResiliencePolicy _repository;
        private readonly IAcquiringBank _acquiringBank;

        public PaymentCommandHandler(IPaymentRepositoryResiliencePolicy repository, IAcquiringBank acquiringBank)
        {
            _repository = repository;
            _acquiringBank = acquiringBank;
        }

        public async Task<PaymentResult> Handle(PaymentCommand command, CancellationToken cancellationToken)
        {
            var acquirinBankPaymentResult = await SendToAcquiringBank(command);

            var creditCard = new Domain.Models.CreditCard(command.Payment.CreditCard.Name, command.Payment.CreditCard.Number,
                command.Payment.CreditCard.Month, command.Payment.CreditCard.Year, command.Payment.CreditCard.CVV);

            var payment = new Domain.Models.Payment(acquirinBankPaymentResult.Id, command.Payment.Amount, creditCard);

            if (acquirinBankPaymentResult.Status == PaymentGateway.Dto.AcquiringBankPayment.PaymentStatus.Success)
            {
                payment.SuccessPayment();
            }
            else
            {
                payment.ErrorPayment();
            }
            
            payment.CreditCard.ApplyMask();

            var result = await _repository.Insert(payment);
            
            return result > 0
                ? await Task.FromResult(new PaymentResult
                {
                    Id = payment.Id,
                    Status = payment.Status == Domain.Models.Status.Success
                        ? PaymentStatus.Success
                        : PaymentStatus.Error
                })
                : null;
        }

        private async Task<AcquirinBankPaymentResult> SendToAcquiringBank(PaymentCommand command)
        {
            var acquirinBankPaymentResult = await _acquiringBank.Send(new AcquirinBankPayment(command.Payment.Amount,
                command.Payment.CreditCard.Name,
                command.Payment.CreditCard.Number, command.Payment.CreditCard.Month, command.Payment.CreditCard.Year,
                command.Payment.CreditCard.CVV));
            
            return acquirinBankPaymentResult;
        }
    }
}