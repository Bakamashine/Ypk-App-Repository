using Application.Dtos.Orders;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Orders.GetOrderListForDefaultUser;

public class GetAllOrderQueryHandlerForDefaultUser : IRequestHandler<GetAllOrderQueryForDefaultUser, OrderListVm>
{
    private readonly IProductsDbContext context;
    private readonly IMapper mapper;

    public GetAllOrderQueryHandlerForDefaultUser(IMapper mapper, IProductsDbContext context)
    {
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<OrderListVm> Handle(GetAllOrderQueryForDefaultUser request, CancellationToken cancellationToken)
    {
        var orders = await context.Orders
            .Include(u => u.User)
            .Include(u => u.Product)
            .Include(u => u.StatusOrder)
            .OrderBy(x => x.Id)
            .Where(x => (x.StatusOrder.StatusName == nameof(StatusOrderEnum.PlaceAn)
                         || x.StatusOrder.StatusName == nameof(StatusOrderEnum.InProgress)
                         || x.StatusOrder.StatusName == nameof(StatusOrderEnum.ReadyForIssue))
                        && x.CustomerId == request.CurrentUserId)
            .ProjectTo<OrderLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new OrderListVm { Orders = orders };
    }
}