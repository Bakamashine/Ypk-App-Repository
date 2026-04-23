using MediatR;

namespace Application.Commands.Users.DeleteUser;

public class DeleteUserCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid CurrentUserId { get; set; }
}