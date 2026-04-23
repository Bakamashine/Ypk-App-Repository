using FluentValidation;

namespace Application.Queries.Ypks.GetYpk;

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