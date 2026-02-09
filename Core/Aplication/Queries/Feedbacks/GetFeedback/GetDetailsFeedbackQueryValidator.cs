using Aplication.Dtos.Feedbacks;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Feedbacks.GetFeedback
{
    public class GetDetailsFeedbackQueryValidator : AbstractValidator<GetDetailsFeedbackQuery>
    {
        public GetDetailsFeedbackQueryValidator()
        {
            RuleFor(getDetailsCourseQuery => getDetailsCourseQuery.Id)
                .NotNull().NotEqual(Guid.Empty);
        }
    }
} 

