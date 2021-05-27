using PaymentGateway.Domain.Models;
using Xunit;

namespace PaymentGateway.Domain.Test
{
    public class CreditcardTests
    {
        [Fact(DisplayName = "Creditcard number Should Be Masked")]
        public void CreateSuccessPaymentShouldHaveSuccessStatus()
        {
            var creditCard = new CreditCard("Natalie Buckley", "4088043836019395", 8, 2030, 159);
            creditCard.Mask();
                
            Assert.Equal("XXXXXXXXXXXX9395", creditCard.Number);
        }
    }
}