using Aplication.Queries.Products.GetProduct;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Orders.GetOrder
{
    public class GetDetailOrderQueryValidator : AbstractValidator<GetDetailOrderQuery>
    {
        public GetDetailOrderQueryValidator()
        {
            RuleFor(getDetailsCourseQuery => getDetailsCourseQuery.Id)
                .NotNull().NotEqual(Guid.Empty);
        }
    }
}
