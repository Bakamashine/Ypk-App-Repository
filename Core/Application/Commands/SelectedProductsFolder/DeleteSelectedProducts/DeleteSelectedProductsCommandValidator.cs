using Application.Commands.SelectedProductsFolder.CreateSelectedProducts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SelectedProductsFolder.DeleteSelectedProducts
{
    public class DeleteSelectedProductsCommandValidator : AbstractValidator<DeleteSelectedProductsCommand>
    {
        public DeleteSelectedProductsCommandValidator()
        {
            RuleFor(createFeedbackCommand => createFeedbackCommand.CurrentUserId)
                .NotEqual(Guid.Empty).NotNull();
            RuleFor(createFeedbackCommand => createFeedbackCommand.SelectedProductId)
                .NotEqual(Guid.Empty).NotNull();
        }
    }
}
