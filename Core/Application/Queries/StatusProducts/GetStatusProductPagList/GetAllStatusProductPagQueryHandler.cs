using Application.Dtos.StatusProducts;
using Application.Extensions;
using Application.Interfaces;
using Application.Queries.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Queries.StatusProducts.GetStatusProductPagList;

public class GetAllStatusProductPagQueryHandler : IRequestHandler<GetAllStatusProductPagQuery, PagedList<StatusProductLookupDto>>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAllStatusProductPagQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<PagedList<StatusProductLookupDto>> Handle(GetAllStatusProductPagQuery request, CancellationToken cancellationToken)
    {
        var query = context.StatusProducts
            .ProjectTo<StatusProductLookupDto>(mapper.ConfigurationProvider)
            .AsQueryable();

        return await query.ToPagedListAsync(request.Page, request.PageSize, cancellationToken);
    }
}
