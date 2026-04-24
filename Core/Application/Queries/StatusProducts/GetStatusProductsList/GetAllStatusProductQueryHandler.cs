using Application.Dtos.StatusProducts;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.StatusProducts.GetStatusProductsList;

public class GetAllStatusProductQueryHandler : IRequestHandler<GetAllStatusProductQuery, StatusProductListVm>
{
    private readonly IProductsDbContext context;
    private readonly IMapper mapper;

    public GetAllStatusProductQueryHandler(IProductsDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<StatusProductListVm> Handle(GetAllStatusProductQuery request, CancellationToken cancellationToken)
    {
        var statusProductLookupDto = await context.StatusProducts
            .ProjectTo<StatusProductLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new StatusProductListVm { StatusProducts = statusProductLookupDto };
    }
}