using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Products.GetYpkListByYpk
{
    public class GetYpkListByYpkQueryValidator : AbstractValidator<GetYpkListByYpkQuery> 
    {
        public GetYpkListByYpkQueryValidator()
        {
            RuleFor(x => x.YpkId).NotEmpty().NotNull();
        }
    }
}
