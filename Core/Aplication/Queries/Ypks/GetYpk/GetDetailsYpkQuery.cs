using Aplication.Dtos.Roles;
using Aplication.Dtos.Ypks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Ypks.GetYpk
{
    public class GetDetailsYpkQuery : IRequest<YpkLookupDto>
    {
        public Guid Id { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}
