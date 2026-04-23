using Application.Dtos.Orders;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Orders.GetOrderList;

public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, OrderListVm>
{
    private readonly IProductsDbContext context;
    private readonly IMapper mapper;

    public GetAllOrderQueryHandler(IMapper mapper, IProductsDbContext context)
    {
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<OrderListVm> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        var orders = await context.Orders
            .Include(u => u.User)
            .Include(u => u.Product)
            .Include(u => u.StatusOrder)
            .OrderBy(x => x.Id)
            .ProjectTo<OrderLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new OrderListVm { Orders = orders };
    }
}