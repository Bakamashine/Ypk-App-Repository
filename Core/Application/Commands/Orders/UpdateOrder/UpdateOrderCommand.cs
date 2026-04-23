using MediatR;

namespace Application.Commands.Orders.UpdateOrder;

public class UpdateOrderCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid CurrentUserId { get; set; }
    public Guid ExecutorId { get; set; }

    public Guid StatusOrderId { get; set; }
    public Guid ProductId { get; set; }

    public string? CustomersComment { get; set; }
    public string? UserComment { get; set; }
}