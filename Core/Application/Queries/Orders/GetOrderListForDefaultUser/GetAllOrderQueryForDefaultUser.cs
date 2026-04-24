using Application.Dtos.Orders;
using MediatR;

namespace Application.Queries.Orders.GetOrderListForDefaultUser;

public class GetAllOrderQueryForDefaultUser : IRequest<OrderListVm>
{
    public Guid CurrentUserId { get; set; }
}