using Aplication.Dtos.Feedbacks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Feedbacks.GetFeedback
{
    public class GetDetailsFeedbackQuery : IRequest<FeedbackLookupDto>
    {
        public Guid Id { get; set; }
    }
}
