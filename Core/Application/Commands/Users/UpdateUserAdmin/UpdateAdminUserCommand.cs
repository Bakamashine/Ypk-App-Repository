using MediatR;

namespace Application.Commands.Users.UpdateUser;

public class UpdateAdminUserCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid CurrentUserId { get; set; }
    public string Fullname { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Guid RoleId { get; set; }
    public Guid? YpkId { get; set; }
    public string? UserInfo { get; set; }
}