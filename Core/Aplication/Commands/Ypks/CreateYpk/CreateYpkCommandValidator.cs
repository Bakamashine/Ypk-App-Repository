using Aplication.Commands.Orders.CreateOrder;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Ypks.CreateYpk
{
    public class CreateYpkCommandValidator : AbstractValidator<CreateYpkCommand>
    {
        public CreateYpkCommandValidator()
        {
            RuleFor(createFeedbackCommand => createFeedbackCommand.YpkName)
               .NotEmpty().NotNull();
        }
    }
}
