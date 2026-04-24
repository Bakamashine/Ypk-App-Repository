using Application.Dtos.StatusProducts;
using MediatR;

namespace Application.Queries.StatusProducts.GetStatusProductsList;

public class GetAllStatusProductQuery : IRequest<StatusProductListVm>
{
}