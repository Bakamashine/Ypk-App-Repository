using Application.Dtos.Products;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Products.GetCreatedProductList;

public class GetAllCreatedProductQueryHandler : IRequestHandler<GetAllCreatedProductQuery, ProductListVm>
{
    private readonly IProductsDbContext context;
    private readonly IMapper mapper;

    public GetAllCreatedProductQueryHandler(IMapper mapper, IProductsDbContext context)
    {
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<ProductListVm> Handle(GetAllCreatedProductQuery request, CancellationToken cancellationToken)
    {
        var products = await context.Products
            .Include(u => u.User)
            .Include(u => u.StatusProduct)
            .Include(u => u.Ypk)
            .OrderBy(x => x.Id)
            .Where(x => x.StatusProduct.StatusName.Contains(nameof(StatusProductEnum.Editing)))
            .ProjectTo<ProductLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ProductListVm { Products = products };
    }
}