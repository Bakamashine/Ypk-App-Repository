using Aplication.Dtos.Feedbacks;
using Aplication.Dtos.Ypks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Feedbacks.GetFeedbackList
{
    public class GetAllFeedbackQuery : IRequest<FeedbackListVm>
    {

    }
}
