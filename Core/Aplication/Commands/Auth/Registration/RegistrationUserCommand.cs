using Aplication.Dtos.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Auth.Registration
{
    public class RegistrationUserCommand : IRequest<TokensDto?>
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
