using Application.Dtos.Auth;
using MediatR;

namespace Application.Commands.Users.UpdateUser;

public class UpdateUserCommand : IRequest<TokensDto>
{
    public Guid Id { get; set; }
    public Guid CurrentUserId { get; set; }
    public string Fullname { get; set; } = string.Empty;
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? UserInfo { get; set; }
    public bool IsActive { get; set; }
}