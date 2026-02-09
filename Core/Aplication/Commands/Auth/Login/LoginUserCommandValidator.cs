using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Auth.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator() 
        {
            RuleFor(loginUserCommand => loginUserCommand.PhoneNumber)
                .NotNull().NotEqual(string.Empty).MaximumLength(30);
            RuleFor(loginUserCommand => loginUserCommand.Password)
                .NotNull().NotEqual(string.Empty).MaximumLength(30);
        }
    }
}
