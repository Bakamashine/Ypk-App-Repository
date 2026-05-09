using Application.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.UpdateCurrentUser
{
    public class UpdateCurrentUserCommand : IRequest<TokenDto?>
    {
        public Guid CurrentUserId { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? UserInfo { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
