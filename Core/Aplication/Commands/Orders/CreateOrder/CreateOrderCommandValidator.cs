using Aplication.Commands.Feedbacks.CreateFeedback;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Orders.CreateOrder
{
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
}