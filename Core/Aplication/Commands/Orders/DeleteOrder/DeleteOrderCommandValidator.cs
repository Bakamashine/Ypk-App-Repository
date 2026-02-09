using Aplication.Commands.Feedbacks.DeleteFeedback;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Orders.DeleteOrder
{
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
}
