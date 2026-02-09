using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Ypks.UpdateYpk
{
    public class UpdateYpkCommand : IRequest
    {
        
        public string YpkName { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}
