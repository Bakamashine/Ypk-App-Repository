using Aplication.Commands.Products.UpdateProduct;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Ypks.UpdateYpk
{
    public class UpdateYpkCommandValidator : AbstractValidator<UpdateYpkCommand>
    {
        public UpdateYpkCommandValidator()
        {
            RuleFor(createFeedbackCommand => createFeedbackCommand.CurrentUserId)
                .NotEqual(Guid.Empty).NotNull();
            RuleFor(createFeedbackCommand => createFeedbackCommand.Id)
                .NotEqual(Guid.Empty).NotNull();
        }
    }
}
