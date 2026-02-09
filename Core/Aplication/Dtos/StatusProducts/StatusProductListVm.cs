using Aplication.Dtos.Ypks;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Dtos.StatusProducts
{
    public class StatusProductListVm
    {
        public IList<StatusProductLookupDto> StatusProducts { get; set; } = new List<StatusProductLookupDto>();
    }
}
