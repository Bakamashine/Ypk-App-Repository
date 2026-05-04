using FluentValidation;

namespace Application.Queries.Base;

public class PagedQueryValidator<T> : AbstractValidator<PagedQuery<T>>
{
    public PagedQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}