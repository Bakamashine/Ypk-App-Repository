using FluentValidation;

namespace Application.Commands.Ypks.UpdateYpk;

public class UpdateYpkCommandValidator : AbstractValidator<UpdateYpkCommand>
{
    public UpdateYpkCommandValidator()
    {
        RuleFor(createFeedbackCommand => createFeedbackCommand.CurrentUserId)
            .NotEqual(Guid.Empty).NotNull();
        RuleFor(createFeedbackCommand => createFeedbackCommand.Id)
            .NotEqual(Guid.Empty).NotNull();
    }
}