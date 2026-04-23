using Application.Dtos.Ypks;
using MediatR;

namespace Application.Queries.Ypks.GetYpk;

public class GetDetailsYpkQuery : IRequest<YpkLookupDto>
{
    public Guid Id { get; set; }
    public Guid CurrentUserId { get; set; }
}