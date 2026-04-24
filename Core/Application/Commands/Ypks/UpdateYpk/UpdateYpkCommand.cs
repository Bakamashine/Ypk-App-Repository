using MediatR;

namespace Application.Commands.Ypks.UpdateYpk;

public class UpdateYpkCommand : IRequest
{
    public string YpkName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid Id { get; set; }
    public Guid CurrentUserId { get; set; }
}