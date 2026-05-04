using Application.Dtos.Orders;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Orders.GetOrderListForManagerHistory;

public class GetAllOrderQueryHandlerForManagerHistory : IRequestHandler<GetAllOrderQueryForManagerHistory, OrderListVm>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAllOrderQueryHandlerForManagerHistory(IMapper mapper, IApplicationDbContext context)
    {
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<OrderListVm> Handle(GetAllOrderQueryForManagerHistory request,
        CancellationToken cancellationToken)
    {
        var currentUser =
            await context.Users.FirstOrDefaultAsync(x => x.Id == request.CurrentUserId, cancellationToken);

        var orders = await context.Orders
            .Include(u => u.User)
            .Include(u => u.Product)
            .Include(u => u.StatusOrder)
            .OrderBy(x => x.Id)
            .Where(x => (x.StatusOrder.StatusName == nameof(StatusOrderEnum.Adopted)
                         || x.StatusOrder.StatusName == nameof(StatusOrderEnum.Cancelled))
                        && x.Product.YpkId == currentUser.YpkId)
            .ProjectTo<OrderLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new OrderListVm { Orders = orders };
    }
}