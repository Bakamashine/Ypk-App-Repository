using Application.Commands.Auth.Registration;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Auth.Registration
{
    public class RegistrationUserValidator : AbstractValidator<RegistrationUserCommand>
    {
        public RegistrationUserValidator() 
        {
            RuleFor(registrationUserCommand => registrationUserCommand.FullName)
               .NotNull().NotEmpty().MaximumLength(30);
            RuleFor(registrationUserCommand => registrationUserCommand.PhoneNumber)
              .MaximumLength(12);
            RuleFor(registrationUserCommand => registrationUserCommand.Password)
              .NotNull().NotEmpty().MaximumLength(60);
        }
    }
}
