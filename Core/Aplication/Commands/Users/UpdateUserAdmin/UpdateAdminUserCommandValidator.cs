using Aplication.Commands.Users.UpdateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Users.UpdateUserAdmin
{
    public class UpdateAdminUserCommandValidator : AbstractValidator<UpdateAdminUserCommand>
    {
        public UpdateAdminUserCommandValidator()
        {
            RuleFor(updateUserCommand => updateUserCommand.Id)
               .NotNull().NotEqual(Guid.Empty);
            RuleFor(updateUserCommand => updateUserCommand.CurrentUserId)
              .NotNull().NotEqual(Guid.Empty);
        }
    }
}
