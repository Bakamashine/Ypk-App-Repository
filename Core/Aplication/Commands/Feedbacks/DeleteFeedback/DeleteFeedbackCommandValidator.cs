using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Feedbacks.DeleteFeedback
{
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
}
