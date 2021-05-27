using System;
using PaymentGateway.Domain.Models;
using Xunit;

namespace PaymentGateway.Domain.Test
{
    public class PaymentDomain
    {
        [Fact(DisplayName = "Create Success Payment Should Have Success Status")]
        public void CreateSuccessPaymentShouldHaveSuccessStatus()
        {
            var creditCard = new CreditCard("Natalie Buckley", "4088043836019395", 8, 2030, 159);
            var payment = new Payment(200, creditCard);
            payment.SuccessPayment();
            
            Assert.Equal(Status.Success, payment.Status);
        }
        
        [Fact(DisplayName = "Create Error Payment Should Have Error Status")]
        public void CreateErrorPaymentShouldHaveErrorStatus()
        {
            var creditCard = new CreditCard("Natalie Buckley", "4088043836019395", 8, 2030, 159);
            var payment = new Payment(200, creditCard);
            payment.ErrorPayment();
            
            Assert.Equal(Status.Error, payment.Status);
        }
    }
}