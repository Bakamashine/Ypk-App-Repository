using Application.Dtos.Orders;
using MediatR;

namespace Application.Queries.Orders.GetOrder;

public class GetDetailOrderQuery : IRequest<OrderLookupDto>
{
    public Guid Id { get; set; }
}