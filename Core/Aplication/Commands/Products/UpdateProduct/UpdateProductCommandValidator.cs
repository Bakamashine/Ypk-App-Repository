using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Products.UpdateProduct
{
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
}
