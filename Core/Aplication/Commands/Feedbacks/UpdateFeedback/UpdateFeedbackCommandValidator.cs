using Aplication.Commands.Feedbacks.CreateFeedback;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Feedbacks.UpdateFeedback
{
    public class UpdateFeedbackCommandValidator : AbstractValidator<UpdateFeedbackCommand>
    {
        public UpdateFeedbackCommandValidator()
        {
            RuleFor(createFeedbackCommand => createFeedbackCommand.FeedbackName)
               .MaximumLength(1000);
            RuleFor(createFeedbackCommand => createFeedbackCommand.CurrentUserId)
                .NotEqual(Guid.Empty).NotNull();
            RuleFor(createFeedbackCommand => createFeedbackCommand.Id)
                .NotEqual(Guid.Empty).NotNull();
            RuleFor(createFeedbackCommand => createFeedbackCommand.Raiting)
                .InclusiveBetween(1, 5);


        }
    }
}
