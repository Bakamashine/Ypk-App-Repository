using Application.Dtos.Products;
using MediatR;

namespace Application.Queries.Products.GetProductList;

public class GetAllProductQuery : IRequest<ProductListVm>
{
}