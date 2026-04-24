using Application.Dtos.Auth;
using MediatR;

namespace Application.Commands.Auth.Login;

public class LoginUserCommand : IRequest<TokensDto?>
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}