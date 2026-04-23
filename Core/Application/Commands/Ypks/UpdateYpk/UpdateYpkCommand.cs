using MediatR;

namespace Application.Commands.Ypks.UpdateYpk;

public class UpdateYpkCommand : IRequest
{
    public string YpkName { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public Guid CurrentUserId { get; set; }
}