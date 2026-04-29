using Application.Dtos.StatusOrders;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.StatusOrders.GetStatusOrdersList;

public class GetAllStatusOrdersQueryHandler : IRequestHandler<GetAllStatusOrdersQuery, StatusOrderListVm>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAllStatusOrdersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<StatusOrderListVm> Handle(GetAllStatusOrdersQuery request, CancellationToken cancellationToken)
    {
        var statusOrdersQuery = await context.StatusOrders
            .ProjectTo<StatusOrderLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new StatusOrderListVm { StatusOrders = statusOrdersQuery };
    }
}