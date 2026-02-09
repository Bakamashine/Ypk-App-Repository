using Application.Common.Queries.Roles.GetRoleList;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Ypks.GetYpkList
{
    public class GetAllYpkQueryValidator : AbstractValidator<GetAllYpkQuery>
    {
        public GetAllYpkQueryValidator()
        {
            
        }
    }
}
