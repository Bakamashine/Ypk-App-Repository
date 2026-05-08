using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.SelectedProductsFolder.GetSelectedProductsPagList
{
    public class GetAllSelectedProductsPagQueryValidator : AbstractValidator<GetAllSelectedProductsPagQuery>
    {
        public GetAllSelectedProductsPagQueryValidator()
        {
            RuleFor(x=>x.CurrentUserId).NotEqual(Guid.Empty).NotNull();
            
        }
    }
}
