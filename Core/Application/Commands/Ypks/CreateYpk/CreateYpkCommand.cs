using MediatR;

namespace Application.Commands.Ypks.CreateYpk;

public class CreateYpkCommand : IRequest<Guid>
{
    public string YpkName { get; set; } = string.Empty;
    public string? Description { get; set; }
}