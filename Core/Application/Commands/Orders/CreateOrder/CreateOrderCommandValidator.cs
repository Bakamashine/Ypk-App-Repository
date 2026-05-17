using FluentValidation;

namespace Application.Commands.Orders.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(createFeedbackCommand => createFeedbackCommand.CustomersComment)
            .MaximumLength(1500);
        RuleFor(createFeedbackCommand => createFeedbackCommand.UserComment)
            .MaximumLength(1500);
        RuleFor(createFeedbackCommand => createFeedbackCommand.CurrentUserId)
            .NotEqual(Guid.Empty).NotNull();
    }
}