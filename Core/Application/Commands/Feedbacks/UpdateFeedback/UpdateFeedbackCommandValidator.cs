using FluentValidation;

namespace Application.Commands.Feedbacks.UpdateFeedback;

public class UpdateFeedbackCommandValidator : AbstractValidator<UpdateFeedbackCommand>
{
    public UpdateFeedbackCommandValidator()
    {
        RuleFor(createFeedbackCommand => createFeedbackCommand.Comment)
            .MaximumLength(1000);
        RuleFor(createFeedbackCommand => createFeedbackCommand.CurrentUserId)
            .NotEqual(Guid.Empty).NotNull();
        RuleFor(createFeedbackCommand => createFeedbackCommand.Id)
            .NotEqual(Guid.Empty).NotNull();
        RuleFor(createFeedbackCommand => createFeedbackCommand.Raiting)
            .InclusiveBetween(1, 5);
    }
}