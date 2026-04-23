using FluentValidation;

namespace Application.Commands.Ypks.DeleteYpk;

public class DeleteYpkCommandValidator : AbstractValidator<DeleteYpkCommand>
{
    public DeleteYpkCommandValidator()
    {
        RuleFor(deleteProductCommand => deleteProductCommand.Id)
            .NotEqual(Guid.Empty).NotNull();
        RuleFor(deleteProductCommand => deleteProductCommand.CurrentUserId)
            .NotEqual(Guid.Empty).NotNull();
    }
}