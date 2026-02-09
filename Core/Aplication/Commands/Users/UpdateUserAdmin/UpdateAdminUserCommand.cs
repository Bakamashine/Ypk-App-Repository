using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Users.UpdateUser
{
    public class UpdateAdminUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid CurrentUserId { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Role Role { get; set; }
        public string? UserInfo { get; set; }
    }
}
