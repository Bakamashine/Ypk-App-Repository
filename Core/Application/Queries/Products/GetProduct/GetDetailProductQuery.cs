using Application.Dtos.Products;
using MediatR;

namespace Application.Queries.Products.GetProduct;

public class GetDetailProductQuery : IRequest<ProductLookupDto>
{
    public Guid Id { get; set; }
}