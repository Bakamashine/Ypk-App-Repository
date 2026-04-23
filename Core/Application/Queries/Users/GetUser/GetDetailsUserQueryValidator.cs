using FluentValidation;

namespace Application.Queries.Users.GetUser;

public class GetDetailsUserQueryValidator : AbstractValidator<GetDetailsUserQuery>
{
    public GetDetailsUserQueryValidator()
    {
        RuleFor(getDetailsUserQuery => getDetailsUserQuery.Id)
            .NotNull().NotEqual(Guid.Empty);
    }
}