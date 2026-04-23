using FluentValidation;

namespace Application.Commands.Feedbacks.CreateFeedback;

public class CreateFeedbackCommandValidator : AbstractValidator<CreateFeedbackCommand>
{
    public CreateFeedbackCommandValidator()
    {
        RuleFor(createFeedbackCommand => createFeedbackCommand.Comment)
            .NotEmpty().MaximumLength(1000).NotNull();
        RuleFor(createFeedbackCommand => createFeedbackCommand.CurrentUserId)
            .NotEqual(Guid.Empty).NotNull();
        RuleFor(createFeedbackCommand => createFeedbackCommand.Raiting)
            .InclusiveBetween(1, 5).NotNull();
    }
}