using MediatR;

namespace Application.Commands.Users.CreateUser;

public class CreateUserCommand : IRequest<Guid>
{
    public Guid CurrentUserId { get; set; }

    public string Fullname { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? UserInfo { get; set; }

    public Guid RoleId { get; set; }
}