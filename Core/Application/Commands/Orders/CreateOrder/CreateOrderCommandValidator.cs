using FluentValidation;

namespace Application.Commands.Orders.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(createFeedbackCommand => createFeedbackCommand.CustomersComment)
            .NotEmpty().MaximumLength(1500).NotNull();
        RuleFor(createFeedbackCommand => createFeedbackCommand.UserComment)
            .NotEmpty().MaximumLength(1500).NotNull();
        RuleFor(createFeedbackCommand => createFeedbackCommand.CurrentUserId)
            .NotEqual(Guid.Empty).NotNull();
    }
}