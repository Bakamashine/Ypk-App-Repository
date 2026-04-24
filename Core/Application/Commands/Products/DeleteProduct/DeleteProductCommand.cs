using MediatR;

namespace Application.Commands.Products.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid CurrentUserId { get; set; }
}