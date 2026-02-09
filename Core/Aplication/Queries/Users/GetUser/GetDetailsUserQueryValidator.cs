using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Users.GetUser
{
    public class GetDetailsUserQueryValidator : AbstractValidator<GetDetailsUserQuery>
    {
        public GetDetailsUserQueryValidator()
        {
            RuleFor(getDetailsUserQuery => getDetailsUserQuery.Id)
                .NotNull().NotEqual(Guid.Empty);
        }
    }
}
