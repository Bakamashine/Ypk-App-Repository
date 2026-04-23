using Application.Dtos.Products;
using MediatR;

namespace Application.Queries.Products.GetProductLis;

public class GetAllProductQuery : IRequest<ProductListVm>
{
}