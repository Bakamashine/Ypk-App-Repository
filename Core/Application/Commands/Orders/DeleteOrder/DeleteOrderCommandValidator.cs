using FluentValidation;

namespace Application.Commands.Orders.DeleteOrder;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(deleteOrderCommand => deleteOrderCommand.Id)
            .NotEqual(Guid.Empty).NotNull();
        RuleFor(deleteOrderCommand => deleteOrderCommand.CurrentUserId)
            .NotEqual(Guid.Empty).NotNull();
    }
}