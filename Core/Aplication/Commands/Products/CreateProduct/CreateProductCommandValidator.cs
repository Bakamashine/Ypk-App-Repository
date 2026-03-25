using Aplication.Commands.Orders.CreateOrder;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Products.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(createFeedbackCommand => createFeedbackCommand.ProductName)
               .NotEmpty().NotNull();
            RuleFor(createFeedbackCommand => createFeedbackCommand.ProductInfo)
               .NotEmpty().NotNull();
            RuleFor(createFeedbackCommand => createFeedbackCommand.Adress)
               .NotEmpty().NotNull();
            RuleFor(createFeedbackCommand => createFeedbackCommand.IsProduct)
               .NotEmpty().NotNull();
        }
    }
}
