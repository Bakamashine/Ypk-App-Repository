using FluentValidation;

namespace Application.Queries.Products.GetYpkListByYpk;

public class GetYpkListByYpkQueryValidator : AbstractValidator<GetYpkListByYpkQuery>
{
    public GetYpkListByYpkQueryValidator()
    {
        RuleFor(x => x.YpkId).NotEmpty().NotNull();
    }
}