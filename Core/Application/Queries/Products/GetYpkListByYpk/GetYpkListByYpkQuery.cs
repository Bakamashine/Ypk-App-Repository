using Application.Dtos.Products;
using MediatR;

namespace Application.Queries.Products.GetYpkListByYpk;

public class GetYpkListByYpkQuery : IRequest<ProductListVm>
{
    public Guid YpkId { get; set; }
}