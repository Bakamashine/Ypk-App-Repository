using FluentValidation;

namespace Application.Queries.Products.GetProduct;

public class GetDetailProductQueryValidator : AbstractValidator<GetDetailProductQuery>
{
    public GetDetailProductQueryValidator()
    {
        RuleFor(getDetailsCourseQuery => getDetailsCourseQuery.Id)
            .NotNull().NotEqual(Guid.Empty);
    }
}