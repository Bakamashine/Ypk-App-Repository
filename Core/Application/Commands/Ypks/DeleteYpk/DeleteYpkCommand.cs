using MediatR;

namespace Application.Commands.Ypks.DeleteYpk;

public class DeleteYpkCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid CurrentUserId { get; set; }
}