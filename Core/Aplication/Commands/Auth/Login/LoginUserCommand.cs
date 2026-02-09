using Aplication.Dtos.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Application.Commands.Auth.Login
{
    public class LoginUserCommand : IRequest<TokensDto?>
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
