using FluentValidation;

namespace Application.Commands.Ypks.CreateYpk;

public class CreateYpkCommandValidator : AbstractValidator<CreateYpkCommand>
{
    public CreateYpkCommandValidator()
    {
        RuleFor(createFeedbackCommand => createFeedbackCommand.YpkName)
            .NotEmpty().NotNull();
    }
}