using Application.Dtos.Products;
using Application.Extensions;
using Application.Interfaces;
using Application.Queries.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Products.GetProductPagList;

public class GetAllProductPagQueryHandler : IRequestHandler<GetAllProductPagQuery, PagedList<ProductLookupDto>>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAllProductPagQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<PagedList<ProductLookupDto>> Handle(GetAllProductPagQuery request, CancellationToken cancellationToken)
    {
        var query = context.Products
            .Include(u => u.User)
            .Include(u => u.StatusProduct)
            .Include(u => u.Ypk)
            .Where(x => x.StatusProduct.StatusName.Contains(nameof(StatusProductEnum.Publish)))
            .OrderBy(x => x.Id)
            .ProjectTo<ProductLookupDto>(mapper.ConfigurationProvider)
            .AsQueryable();

        return await query.ToPagedListAsync(request.Page, request.PageSize, cancellationToken);
    }
}
