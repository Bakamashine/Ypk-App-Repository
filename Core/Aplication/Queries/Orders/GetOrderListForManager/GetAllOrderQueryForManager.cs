using Aplication.Dtos.Orders;
using Aplication.Dtos.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Orders.GetOrderListForManager
{
    public class GetAllOrderQueryForManager : IRequest<OrderListVm>
    {
        public Guid CurrentUserId {  get; set; }
    }
}
