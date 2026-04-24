using FluentValidation;

namespace Application.Queries.Feedbacks.GetFeedback;

public class GetDetailsFeedbackQueryValidator : AbstractValidator<GetDetailsFeedbackQuery>
{
    public GetDetailsFeedbackQueryValidator()
    {
        RuleFor(getDetailsCourseQuery => getDetailsCourseQuery.Id)
            .NotNull().NotEqual(Guid.Empty);
    }
}