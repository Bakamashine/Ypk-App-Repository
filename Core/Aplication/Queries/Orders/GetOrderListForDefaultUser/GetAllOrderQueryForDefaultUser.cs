using Aplication.Dtos.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Orders.GetOrderListForDefaultUser
{
    public class GetAllOrderQueryForDefaultUser : IRequest<OrderListVm>
    {
        public Guid CurrentUserId { get; set; }
    }
}
