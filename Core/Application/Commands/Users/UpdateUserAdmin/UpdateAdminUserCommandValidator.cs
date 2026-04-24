using Application.Commands.Users.UpdateUser;
using FluentValidation;

namespace Application.Commands.Users.UpdateUserAdmin;

public class UpdateAdminUserCommandValidator : AbstractValidator<UpdateAdminUserCommand>
{
    public UpdateAdminUserCommandValidator()
    {
        RuleFor(updateUserCommand => updateUserCommand.Id)
            .NotNull().NotEqual(Guid.Empty);
        RuleFor(updateUserCommand => updateUserCommand.CurrentUserId)
            .NotNull().NotEqual(Guid.Empty);
    }
}