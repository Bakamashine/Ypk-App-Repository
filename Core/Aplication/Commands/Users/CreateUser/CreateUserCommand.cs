using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Users.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public Guid CurrentUserId { get; set; }

        public string Fullname { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? UserInfo { get; set; }

        public Role? Role { get; set; }
    }
}
