using Application.Dtos.SelectedProductsFolder;
using Application.Dtos.StatusOrders;
using Application.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.SelectedProductsFolder.GetSelectedProductsPagList
{
    public class GetAllSelectedProductsPagQuery : PagedQuery<SelectedProductsLookupDto>
    {
        public Guid CurrentUserId { get; set; }
    }
}
