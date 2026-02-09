using Aplication.Dtos.StatusOrders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.StatusOrders.GetStatusOrdersList
{
    public class GetAllStatusOrdersQuery : IRequest<StatusOrderListVm>
    {

    }
}
