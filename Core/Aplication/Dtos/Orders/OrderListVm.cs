using Aplication.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Dtos.Orders
{
    public class OrderListVm
    {
        public IList<OrderLookupDto> Orders { get; set; } = new List<OrderLookupDto>();
    }
}
