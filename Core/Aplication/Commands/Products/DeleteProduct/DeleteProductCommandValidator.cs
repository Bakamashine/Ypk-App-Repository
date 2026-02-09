using Aplication.Commands.Feedbacks.DeleteFeedback;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Products.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(deleteProductCommand => deleteProductCommand.Id)
                .NotEqual(Guid.Empty).NotNull();
            RuleFor(deleteProductCommand => deleteProductCommand.CurrentUserId)
                .NotEqual(Guid.Empty).NotNull();
        }
    }
}
