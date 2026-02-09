using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Users.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(updateUserCommand => updateUserCommand.Id)
               .NotNull().NotEqual(Guid.Empty);
            RuleFor(updateUserCommand => updateUserCommand.CurrentUserId)
              .NotNull().NotEqual(Guid.Empty);
        }
    }
}
