using Aplication.Commands.Ypks.CreateYpk;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Users.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(createFeedbackCommand => createFeedbackCommand.CurrentUserId)
               .NotEqual(Guid.Empty).NotNull();
            RuleFor(createFeedbackCommand => createFeedbackCommand.Fullname)
              .NotEmpty().MaximumLength(150).NotNull();
            RuleFor(createFeedbackCommand => createFeedbackCommand.Password)
              .NotEmpty().NotNull();
            RuleFor(createFeedbackCommand => createFeedbackCommand.PhoneNumber)
             .NotEmpty().MaximumLength(12).NotNull();
        }
    }
}