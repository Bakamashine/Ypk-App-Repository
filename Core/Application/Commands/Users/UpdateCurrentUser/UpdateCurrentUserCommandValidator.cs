using Application.Commands.Users.UpdateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.UpdateCurrentUser
{
    public class UpdateCurrentUserCommandValidator : AbstractValidator<UpdateCurrentUserCommand>
    {
        public UpdateCurrentUserCommandValidator()
        {
            RuleFor(updateUserCommand => updateUserCommand.CurrentUserId)
                .NotNull().NotEqual(Guid.Empty);
        }
    }
}
