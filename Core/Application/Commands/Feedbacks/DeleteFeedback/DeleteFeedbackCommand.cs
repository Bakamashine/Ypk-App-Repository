using MediatR;

namespace Application.Commands.Feedbacks.DeleteFeedback;

public class DeleteFeedbackCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid CurrentUserId { get; set; }
}