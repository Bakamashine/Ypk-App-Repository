using MediatR;

namespace Application.Commands.Orders.DeleteOrder;

public class DeleteOrderCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid CurrentUserId { get; set; }
}