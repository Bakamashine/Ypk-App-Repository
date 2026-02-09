using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Orders.UpdateOrder
{
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
}
