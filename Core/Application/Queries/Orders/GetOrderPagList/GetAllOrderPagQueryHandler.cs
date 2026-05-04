using Application.Dtos.Orders;
using Application.Extensions;
using Application.Interfaces;
using Application.Queries.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Queries.Orders.GetOrderPagList;

class GetAllOrderPagQueryHandler : IRequestHandler<GetAllOrderPagQuery, PagedList<OrderLookupDto>>
{
     private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAllOrderPagQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    public async Task<PagedList<OrderLookupDto>> Handle(GetAllOrderPagQuery request, CancellationToken cancellationToken)
    {
        var query = context.Orders
        .ProjectTo<OrderLookupDto>(mapper.ConfigurationProvider)
        .AsQueryable();
        return await query.ToPagedListAsync(request.Page, request.PageSize, cancellationToken);
    }
}