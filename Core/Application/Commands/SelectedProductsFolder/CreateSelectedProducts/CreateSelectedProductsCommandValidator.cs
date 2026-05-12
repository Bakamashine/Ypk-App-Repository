using Application.Commands.Users.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SelectedProductsFolder.CreateSelectedProducts
{
    public class CreateSelectedProductsCommandValidator : AbstractValidator<CreateSelectedProductsCommand>
    {
        public CreateSelectedProductsCommandValidator()
        {
            RuleFor(createFeedbackCommand => createFeedbackCommand.CurrentUserId)
                .NotEqual(Guid.Empty).NotNull();
            RuleFor(createFeedbackCommand => createFeedbackCommand.ProductId)
                .NotEqual(Guid.Empty).NotNull();
        }
    }
}
