using FluentValidation;

namespace Application.Commands.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(createFeedbackCommand => createFeedbackCommand.ProductName)
            .NotEmpty().NotNull();
        RuleFor(createFeedbackCommand => createFeedbackCommand.ProductInfo)
            .NotEmpty().NotNull();
        RuleFor(createFeedbackCommand => createFeedbackCommand.Adress)
            .NotEmpty().NotNull();
        RuleFor(createFeedbackCommand => createFeedbackCommand.IsProduct)
            .NotNull();
    }
}