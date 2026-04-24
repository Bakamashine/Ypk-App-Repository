using Application.Dtos.Ypks;
using MediatR;

namespace Application.Queries.Ypks.GetYpkList;

public class GetAllYpkQuery : IRequest<YpkListVm>
{
}