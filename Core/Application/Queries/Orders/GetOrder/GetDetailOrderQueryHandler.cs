using Application.Dtos.Orders;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Orders.GetOrder;

public class GetDetailOrderQueryHandler : IRequestHandler<GetDetailOrderQuery, OrderLookupDto>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetDetailOrderQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<OrderLookupDto> Handle(GetDetailOrderQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Orders
            .Include(p => p.User)
            .Include(p => p.Product)
            .Include(p => p.StatusOrder)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return mapper.Map<OrderLookupDto>(entity);
    }
}