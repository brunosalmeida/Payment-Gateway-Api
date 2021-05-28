using System;
using FluentValidation;
using PaymentGateway.Dto.Request;

namespace PaymentGateway.Api.Validadors
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(u => u.Amount).NotNull().WithMessage(" Amount can not be null");
            RuleFor(u => u.Amount).NotEmpty().WithMessage("Amount can not be empty");
        }
    }
    
    public class CreditCardValidator : AbstractValidator<CreditCard>
    {
        public CreditCardValidator()
        {
            RuleFor(u => u.Name).NotNull().WithMessage("Creditcard's name can not be null");
            RuleFor(u => u.Name).NotEmpty().WithMessage("Creditcard's name can not be empty");
            
            RuleFor(u => u.Number).NotNull().WithMessage("Creditcard's number can not be null");
            RuleFor(u => u.Number).CreditCard();

            RuleFor(u => u.Month).NotEqual(0).WithMessage("Creditcard's expiration month can not be zero");
            RuleFor(u => u.Month).LessThan(12).WithMessage("Creditcard's expiration month can not be greater than 12");
            RuleFor(u => u.Month).GreaterThan(1).WithMessage("Creditcard's expiration month can not be less than 1");

            var year = DateTime.UtcNow.Year;
            RuleFor(u => u.Year).LessThan(year).WithMessage($"Creditcard's expiration month can not be greater than {year}");
            
            RuleFor(u => u.CVV).Length(3).WithMessage($"Creditcard's CVV must have 3 digits");
            RuleFor(u => u.CVV).Matches("^[0-9]+$").WithMessage($"Creditcard's CVV must have only numbers");
            
        }
    }
}