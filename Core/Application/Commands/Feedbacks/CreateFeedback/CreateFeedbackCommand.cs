using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.Feedbacks.CreateFeedback;

public class CreateFeedbackCommand : IRequest<Guid>
{
    public Guid CurrentUserId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int Raiting { get; set; }
    public IFormFile? Image { get; set; }
}