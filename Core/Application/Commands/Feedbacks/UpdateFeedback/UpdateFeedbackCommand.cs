using MediatR;

namespace Application.Commands.Feedbacks.UpdateFeedback;

public class UpdateFeedbackCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid CurrentUserId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int Raiting { get; set; }
}