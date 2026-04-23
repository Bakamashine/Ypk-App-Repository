using FluentValidation;

namespace Application.Commands.Feedbacks.DeleteFeedback;

public class DeleteFeedbackCommandValidator : AbstractValidator<DeleteFeedbackCommand>
{
    public DeleteFeedbackCommandValidator()
    {
        RuleFor(deleteFeedbackCommand => deleteFeedbackCommand.Id)
            .NotEqual(Guid.Empty).NotNull();
        RuleFor(deleteFeedbackCommand => deleteFeedbackCommand.CurrentUserId)
            .NotEqual(Guid.Empty).NotNull();
    }
}