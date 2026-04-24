using MediatR;

namespace Application.Commands.Orders.CreateOrder;

public class CreateOrderCommand : IRequest<Guid>
{
    public Guid CurrentUserId { get; set; }
    public Guid ProductId { get; set; }


    public string? CustomersComment { get; set; }
    public string? UserComment { get; set; }
}