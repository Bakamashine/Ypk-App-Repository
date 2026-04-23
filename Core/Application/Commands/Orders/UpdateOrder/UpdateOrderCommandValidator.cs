using FluentValidation;

namespace Application.Commands.Orders.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(createFeedbackCommand => createFeedbackCommand.CustomersComment)
            .MaximumLength(1500);
        RuleFor(createFeedbackCommand => createFeedbackCommand.UserComment)
            .MaximumLength(1500);
        RuleFor(createFeedbackCommand => createFeedbackCommand.CurrentUserId)
            .NotEqual(Guid.Empty).NotNull();
        RuleFor(createFeedbackCommand => createFeedbackCommand.Id)
            .NotEqual(Guid.Empty).NotNull();
    }
}