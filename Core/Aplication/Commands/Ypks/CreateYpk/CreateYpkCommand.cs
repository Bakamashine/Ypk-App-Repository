using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Ypks.CreateYpk
{
    public class CreateYpkCommand : IRequest<Guid>
    {
        public string YpkName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
