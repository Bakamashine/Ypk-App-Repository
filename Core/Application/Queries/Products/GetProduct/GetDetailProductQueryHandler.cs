using Application.Dtos.Products;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Products.GetProduct;

public class GetDetailProductQueryHandler : IRequestHandler<GetDetailProductQuery, ProductLookupDto>
{
    private readonly IProductsDbContext context;
    private readonly IMapper mapper;

    public GetDetailProductQueryHandler(IProductsDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ProductLookupDto> Handle(GetDetailProductQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Products
            .Include(p => p.User)
            .Include(p => p.Ypk)
            .Include(p => p.StatusProduct)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return mapper.Map<ProductLookupDto>(entity);
    }
}