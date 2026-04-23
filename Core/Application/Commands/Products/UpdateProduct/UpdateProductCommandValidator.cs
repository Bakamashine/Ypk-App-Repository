using FluentValidation;

namespace Application.Commands.Products.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(createFeedbackCommand => createFeedbackCommand.CurrentUserId)
            .NotEqual(Guid.Empty).NotNull();
        RuleFor(createFeedbackCommand => createFeedbackCommand.Id)
            .NotEqual(Guid.Empty).NotNull();
    }
}