using Application.Dtos.Products;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Products.GetYpkListByYpk;

public class GetYpkListByYpkQueryHandler : IRequestHandler<GetYpkListByYpkQuery, ProductListVm>
{
    private readonly IProductsDbContext context;
    private readonly IMapper mapper;

    public GetYpkListByYpkQueryHandler(IMapper mapper, IProductsDbContext context)
    {
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<ProductListVm> Handle(GetYpkListByYpkQuery request, CancellationToken cancellationToken)
    {
        var products = await context.Products
            .Include(u => u.User)
            .Include(u => u.StatusProduct)
            .Include(u => u.Ypk)
            .Where(x => x.StatusProduct.StatusName.Contains(nameof(StatusProductEnum.Publish)) &&
                        request.YpkId == x.YpkId)
            .OrderBy(x => x.Id)
            .ProjectTo<ProductLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ProductListVm { Products = products };
    }
}