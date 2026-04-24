using Application.Dtos.Auth;
using MediatR;

namespace Application.Commands.Auth.Registration;

public class RegistrationUserCommand : IRequest<TokensDto?>
{
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}