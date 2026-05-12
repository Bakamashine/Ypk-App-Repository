using Application.Dtos.StatusOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.SelectedProductsFolder
{
    public class SelectedProductsListVm
    {
        public IList<SelectedProductsLookupDto> SelectedProducts { get; set; } = new List<SelectedProductsLookupDto>();
    }
}
