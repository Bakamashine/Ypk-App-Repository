using FluentValidation;

namespace Application.Commands.Products.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(deleteProductCommand => deleteProductCommand.Id)
            .NotEqual(Guid.Empty).NotNull();
        RuleFor(deleteProductCommand => deleteProductCommand.CurrentUserId)
            .NotEqual(Guid.Empty).NotNull();
    }
}