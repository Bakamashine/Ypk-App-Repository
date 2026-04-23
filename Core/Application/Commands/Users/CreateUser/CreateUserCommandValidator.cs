using FluentValidation;

namespace Application.Commands.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(createFeedbackCommand => createFeedbackCommand.CurrentUserId)
            .NotEqual(Guid.Empty).NotNull();
        RuleFor(createFeedbackCommand => createFeedbackCommand.Fullname)
            .NotEmpty().MaximumLength(150).NotNull();
        RuleFor(createFeedbackCommand => createFeedbackCommand.Password)
            .NotEmpty().NotNull();
        RuleFor(createFeedbackCommand => createFeedbackCommand.PhoneNumber)
            .NotEmpty().MaximumLength(12).NotNull();
    }
}