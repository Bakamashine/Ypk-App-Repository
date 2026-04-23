using Application.Dtos.Orders;
using MediatR;

namespace Application.Queries.Orders.GetOrderListForManagerHistory;

public class GetAllOrderQueryForManagerHistory : IRequest<OrderListVm>
{
    public Guid CurrentUserId { get; set; }
}