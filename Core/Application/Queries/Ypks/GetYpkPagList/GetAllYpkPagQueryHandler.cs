using Application.Dtos.Ypks;
using Application.Extensions;
using Application.Interfaces;
using Application.Queries.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Queries.Ypks.GetYpkPagList;

public class GetAllYpkPagQueryHandler : IRequestHandler<GetAllYpkPagQuery, PagedList<YpkLookupDto>>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAllYpkPagQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<PagedList<YpkLookupDto>> Handle(GetAllYpkPagQuery request, CancellationToken cancellationToken)
    {
        var query = context.Ypks
            .Where(x => x.IsActive == true)
            .ProjectTo<YpkLookupDto>(mapper.ConfigurationProvider)
            .AsQueryable();

        return await query.ToPagedListAsync(request.Page, request.PageSize, cancellationToken);
    }
}
