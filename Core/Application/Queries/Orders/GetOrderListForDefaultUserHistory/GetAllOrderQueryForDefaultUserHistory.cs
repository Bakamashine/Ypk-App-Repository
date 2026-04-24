using Application.Dtos.Orders;
using MediatR;

namespace Application.Queries.Orders.GetOrderListForDefaultUserHistory;

public class GetAllOrderQueryForDefaultUserHistory : IRequest<OrderListVm>
{
    public Guid CurrentUserId { get; set; }
}