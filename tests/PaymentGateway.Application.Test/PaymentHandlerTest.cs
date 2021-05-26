using System;
using System.Threading;
using System.Threading.Tasks;
using Paymentgateway.Application;
using PaymentGateway.Dto.Request;
using PaymentGateway.Dto.Response;
using Xunit;

namespace PaymentGateway.Application.Test
{
    public class PaymentHandlerTest
    {
        [Fact(DisplayName = "Add new payment and returns a successful status")]
        public async Task AddPaymentShouldReturnSuccessfullStatus()
        {
            var request = new Payment
            {
                Amount = 200,
                CreditCard = new CreditCard
                {
                    Name = "Natalie Buckley",
                    Number = "4088043836019395",
                    Month = 8,
                    Year = 2030,
                    CVV = 159
                }
            };

            var command = new PaymentCommand(request);
            
            var handler = new PaymentCommandHandler();
            var result = await handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(PaymentStatus.Success, result.Status);
        }
    }
}