using System;
using FluentValidation;
using PaymentGateway.Dto.Request;

namespace PaymentGateway.Api.Validadors
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(u => u.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
            RuleFor(u => u.CreditCard).SetValidator(new CreditCardValidator());
        }
    }
    
    public class CreditCardValidator : AbstractValidator<CreditCard>
    {
        public CreditCardValidator()
        {
            RuleFor(u => u.Name).NotNull().WithMessage("Creditcard's name can not be null");
            RuleFor(u => u.Name).NotEmpty().WithMessage("Creditcard's name can not be empty");
            
            RuleFor(u => u.Number).NotNull().WithMessage("Creditcard's number can not be null");
            RuleFor(u => u.Number).CreditCard().WithMessage("Is not a valid credit card number.");

            RuleFor(u => u.Month).NotEqual(0).WithMessage("Creditcard's expiration month can not be zero");
            RuleFor(u => u.Month).LessThanOrEqualTo(12).WithMessage("Creditcard's expiration month can not be greater than 12");
            RuleFor(u => u.Month).GreaterThanOrEqualTo(1).WithMessage("Creditcard's expiration month can not be less than 1");

            var year = DateTime.UtcNow.Year;
            RuleFor(u => u.Year).GreaterThanOrEqualTo(year).WithMessage($"Creditcard's expiration month can not be less than {year}");
            
            RuleFor(u => u.CVV).Length(3).WithMessage($"Creditcard's CVV must have 3 digits");
            RuleFor(u => u.CVV).Matches("^[0-9]+$").WithMessage($"Creditcard's CVV must have only numbers");
            
        }
    }
}