using Aplication.Dtos.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Users.GetUser
{
    public class GetDetailsUserQuery : IRequest<UserLookupDto>
    {
        public Guid Id { get; set; }
    }
}
