using Aplication.Commands.Products.DeleteProduct;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Ypks.DeleteYpk
{
    public class DeleteYpkCommandValidator : AbstractValidator<DeleteYpkCommand>
    {
        public DeleteYpkCommandValidator()
        {
            RuleFor(deleteProductCommand => deleteProductCommand.Id)
                .NotEqual(Guid.Empty).NotNull();
            RuleFor(deleteProductCommand => deleteProductCommand.CurrentUserId)
                .NotEqual(Guid.Empty).NotNull();
        }
    }
}
