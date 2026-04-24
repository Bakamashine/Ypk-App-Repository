using Application.Dtos.Products;
using MediatR;

namespace Application.Queries.Products.GetCreatedProductList;

public class GetAllCreatedProductQuery : IRequest<ProductListVm>
{
}