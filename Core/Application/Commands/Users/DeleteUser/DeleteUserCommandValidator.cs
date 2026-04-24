using FluentValidation;

namespace Application.Commands.Users.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(deleteUserCommand => deleteUserCommand.Id)
            .NotEqual(Guid.Empty).NotNull();
        RuleFor(deleteUserCommand => deleteUserCommand.CurrentUserId)
            .NotEqual(Guid.Empty).NotNull();
    }
}