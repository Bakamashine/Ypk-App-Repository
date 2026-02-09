using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Dtos.Roles
{
    public class RoleListVm
    {
        public IList<RoleLookupDto> Roles { get; set; } = new List<RoleLookupDto>();
    }
}
