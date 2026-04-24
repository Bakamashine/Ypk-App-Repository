using Application.Dtos.Orders;
using MediatR;

namespace Application.Queries.Orders.GetOrderListForManager;

public class GetAllOrderQueryForManager : IRequest<OrderListVm>
{
    public Guid CurrentUserId { get; set; }
}