using Aplication.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Dtos.Ypks
{
    public class YpkListVm
    {
        public IList<YpkLookupDto> Ypks { get; set; } = new List<YpkLookupDto>();
    }
}
