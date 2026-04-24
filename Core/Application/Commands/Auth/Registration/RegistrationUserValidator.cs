using FluentValidation;

namespace Application.Commands.Auth.Registration;

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