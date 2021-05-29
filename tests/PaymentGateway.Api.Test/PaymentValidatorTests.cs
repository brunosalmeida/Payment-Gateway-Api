using System;
using System.Threading.Tasks;
using PaymentGateway.Api.Validadors;
using Xunit;
using PaymentGateway.Dto.Request;

namespace PaymentGateway.Api.Test
{
    public class PaymentValidatorTests
    {
        [Fact(DisplayName = "Add new payment with amount equals 0 should be invalid.")]
        public async Task AddNewPaymentWithAmountZeroShouldReturnsInvalid()
        {
            var payment = new Payment
            {
                Amount = 0,
                CreditCard = new CreditCard
                {
                    Name = "Natalie Buckley",
                    Number = "4088043836019395",
                    Month = 8,
                    Year = 2030,
                    CVV = "159"
                }
            };

            var validator = new PaymentValidator();
            var validationResult = await validator.ValidateAsync(payment, default);
         
            Assert.False(validationResult.IsValid);
        }
        
        [Fact(DisplayName = "Add new payment with no name should be invalid.")]
        public async Task AddNewPaymentWithNoNameShouldReturnsInvalid()
        {
            var payment = new Payment
            {
                Amount = 1,
                CreditCard = new CreditCard
                {
                    Name = string.Empty,
                    Number = "4088043836019395",
                    Month = 8,
                    Year = 2030,
                    CVV = "159"
                }
            };

            var validator = new PaymentValidator();
            var validationResult = await validator.ValidateAsync(payment, default);
         
            Assert.False(validationResult.IsValid);
        }
        
        [Fact(DisplayName = "Add new payment with no creditcard number should be invalid.")]
        public async Task AddNewPaymentWithNoCreditcardNumberShouldReturnsInvalid()
        {
            var payment = new Payment
            {
                Amount = 0,
                CreditCard = new CreditCard
                {
                    Name = "Natalie Buckley",
                    Number = string.Empty,
                    Month = 8,
                    Year = 2030,
                    CVV = "159"
                }
            };

            var validator = new PaymentValidator();
            var validationResult = await validator.ValidateAsync(payment, default);
         
            Assert.False(validationResult.IsValid);
        }
        
        [Fact(DisplayName = "Add new payment with invalid creditcard number should be invalid.")]
        public async Task AddNewPaymentWithInvalidCreditcardNumberShouldReturnsInvalid()
        {
            var payment = new Payment
            {
                Amount = 0,
                CreditCard = new CreditCard
                {
                    Name = "Natalie Buckley",
                    Number = "4088043836395",
                    Month = 8,
                    Year = 2030,
                    CVV = "159"
                }
            };

            var validator = new PaymentValidator();
            var validationResult = await validator.ValidateAsync(payment, default);
         
            Assert.False(validationResult.IsValid);
        }
        
        [Theory(DisplayName = "Add new payment with invalid creditcard expiration month should be invalid.")]
        [InlineData(0)]
        [InlineData(13)]
        public async Task AddNewPaymentWithInvalidCreditcardExpirationMonthShouldReturnsInvalid(int year)
        {
            var payment = new Payment
            {
                Amount = 0,
                CreditCard = new CreditCard
                {
                    Name = "Natalie Buckley",
                    Number = "4088043836019395",
                    Month = year,
                    Year = 2030,
                    CVV = "159"
                }
            };

            var validator = new PaymentValidator();
            var validationResult = await validator.ValidateAsync(payment, default);
         
            Assert.False(validationResult.IsValid);
        }
        
        [Fact(DisplayName = "Add new payment with past year credit card expiration should be invalid.")]
        public async Task AddNewPaymentWithPastYearCreditcardExpirationShouldReturnsInvalid()
        {
            var payment = new Payment
            {
                Amount = 0,
                CreditCard = new CreditCard
                {
                    Name = "Natalie Buckley",
                    Number = "4088043836019395",
                    Month = 0,
                    Year = DateTime.UtcNow.Year -1,
                    CVV = "159"
                }
            };

            var validator = new PaymentValidator();
            var validationResult = await validator.ValidateAsync(payment, default);
         
            Assert.False(validationResult.IsValid);
        }
        
        [Theory(DisplayName = "Add new payment with invalid credit card CVV number should be invalid.")]
        [InlineData("xyz")]
        [InlineData("13000")]
        [InlineData("12")]
        [InlineData("1")]
        [InlineData("1xx")]
        [InlineData("12x")]
        [InlineData("")]
        public async Task AddNewPaymentWithInvalidCreditcardCvvNumberShouldReturnsInvalid(string cvv)
        {
            var payment = new Payment
            {
                Amount = 0,
                CreditCard = new CreditCard
                {
                    Name = "Natalie Buckley",
                    Number = "4088043836019395",
                    Month = 0,
                    Year = DateTime.UtcNow.Year -1,
                    CVV = cvv
                }
            };

            var validator = new PaymentValidator();
            var validationResult = await validator.ValidateAsync(payment, default);
          
            Assert.False(validationResult.IsValid);
        }
    }
}