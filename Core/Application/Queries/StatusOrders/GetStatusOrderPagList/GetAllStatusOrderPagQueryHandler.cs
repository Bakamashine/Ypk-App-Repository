using Application.Dtos.StatusOrders;
using Application.Extensions;
using Application.Interfaces;
using Application.Queries.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Queries.StatusOrders.GetStatusOrderPagList;

public class GetAllStatusOrderPagQueryHandler : IRequestHandler<GetAllStatusOrderPagQuery, PagedList<StatusOrderLookupDto>>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAllStatusOrderPagQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<PagedList<StatusOrderLookupDto>> Handle(GetAllStatusOrderPagQuery request, CancellationToken cancellationToken)
    {
        var query = context.StatusOrders
            .ProjectTo<StatusOrderLookupDto>(mapper.ConfigurationProvider)
            .AsQueryable();

        return await query.ToPagedListAsync(request.Page, request.PageSize, cancellationToken);
    }
}
