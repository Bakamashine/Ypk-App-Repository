using Application.Dtos.Orders;
using MediatR;

namespace Application.Queries.Orders.GetOrderList;

public class GetAllOrderQuery : IRequest<OrderListVm>
{
}