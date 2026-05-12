using Application.Queries.SelectedProductsFolder.GetSelectedProductsPagList;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.SelectedProductsFolder.GetSelectedProductsList
{
    public class GetAllSelectedProductsQueryValidator : AbstractValidator<GetAllSelectedProductsQuery>
    {
        public GetAllSelectedProductsQueryValidator()
        {
            RuleFor(x => x.CurrentUserId).NotEqual(Guid.Empty).NotNull();

        }
    }
}
