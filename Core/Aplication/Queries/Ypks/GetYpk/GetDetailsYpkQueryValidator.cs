using Application.Common.Queries.Roles.GetRole;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Ypks.GetYpk
{
    public class GetDetailsYpkQueryValidator : AbstractValidator<GetDetailsYpkQuery>
    {
        public GetDetailsYpkQueryValidator()
        {
            RuleFor(getDetailsRoleQuery => getDetailsRoleQuery.Id)
                .NotNull().NotEqual(Guid.Empty);
            RuleFor(getDetailsRoleQuery => getDetailsRoleQuery.CurrentUserId)
               .NotNull().NotEqual(Guid.Empty);
        }
    }
}
