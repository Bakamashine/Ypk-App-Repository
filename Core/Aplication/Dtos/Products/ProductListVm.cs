using Aplication.Dtos.StatusProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Dtos.Products
{
    public class ProductListVm
    {
        public IList<ProductLookupDto> Products { get; set; } = new List<ProductLookupDto>();
    }
}
