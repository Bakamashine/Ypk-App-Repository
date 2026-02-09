using Aplication.Dtos.StatusOrders;
using Aplication.Dtos.StatusProducts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.StatusProducts.GetStatusProductsList
{
    public class GetAllStatusProductQuery : IRequest<StatusProductListVm>
    {

    }
}
