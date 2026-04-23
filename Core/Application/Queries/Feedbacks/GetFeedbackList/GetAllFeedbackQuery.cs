using Application.Dtos.Feedbacks;
using MediatR;

namespace Application.Queries.Feedbacks.GetFeedbackList;

public class GetAllFeedbackQuery : IRequest<FeedbackListVm>
{
}