using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Products.GetProduct
{
    public class GetDetailProductQueryValidator : AbstractValidator<GetDetailProductQuery>
    {
        public GetDetailProductQueryValidator()
        {
            RuleFor(getDetailsCourseQuery => getDetailsCourseQuery.Id)
                .NotNull().NotEqual(Guid.Empty);
        }
    }
}
