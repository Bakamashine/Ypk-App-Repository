using FluentValidation;

namespace Application.Queries.Orders.GetOrder;

public class GetDetailOrderQueryValidator : AbstractValidator<GetDetailOrderQuery>
{
    public GetDetailOrderQueryValidator()
    {
        RuleFor(getDetailsCourseQuery => getDetailsCourseQuery.Id)
            .NotNull().NotEqual(Guid.Empty);
    }
}