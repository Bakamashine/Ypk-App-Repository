using FluentValidation;

namespace Application.Commands.Users.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(updateUserCommand => updateUserCommand.Id)
            .NotNull().NotEqual(Guid.Empty);
        RuleFor(updateUserCommand => updateUserCommand.CurrentUserId)
            .NotNull().NotEqual(Guid.Empty);
    }
}