using Aplication.Dtos.StatusProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Dtos.StatusOrders
{
    public class StatusOrderListVm
    {
        public IList<StatusOrderLookupDto> StatusOrders { get; set; } = new List<StatusOrderLookupDto>();
    }
}
