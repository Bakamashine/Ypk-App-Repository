using Application.Dtos.StatusOrders;
using MediatR;

namespace Application.Queries.StatusOrders.GetStatusOrdersList;

public class GetAllStatusOrdersQuery : IRequest<StatusOrderListVm>
{
}