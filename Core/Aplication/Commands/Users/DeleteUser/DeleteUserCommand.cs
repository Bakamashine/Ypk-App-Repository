using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Users.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}
