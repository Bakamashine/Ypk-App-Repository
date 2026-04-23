using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.Products.UpdateProduct;

public class UpdateProductCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid CurrentUserId { get; set; }

    public string ProductName { get; set; } = string.Empty;
    public string ProductInfo { get; set; } = string.Empty;
    public decimal ProductCost { get; set; }
    public bool IsProduct { get; set; }
    public string Adress { get; set; } = string.Empty;
    public IFormFile? Photo { get; set; }

    public Guid YpkId { get; set; }
    public Guid StatusProductId { get; set; }
}