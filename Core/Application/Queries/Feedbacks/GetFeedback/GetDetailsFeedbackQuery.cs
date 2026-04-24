using Application.Dtos.Feedbacks;
using MediatR;

namespace Application.Queries.Feedbacks.GetFeedback;

public class GetDetailsFeedbackQuery : IRequest<FeedbackLookupDto>
{
    public Guid Id { get; set; }
}