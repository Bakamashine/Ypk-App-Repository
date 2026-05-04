using Application.Dtos.Products;
using Application.Extensions;
using Application.Interfaces;
using Application.Queries.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Products.GetCreatedProductPagList;

public class GetAllCreatedProductPagQueryHandler : IRequestHandler<GetAllCreatedProductPagQuery, PagedList<ProductLookupDto>>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAllCreatedProductPagQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<PagedList<ProductLookupDto>> Handle(GetAllCreatedProductPagQuery request, CancellationToken cancellationToken)
    {
        var query = context.Products
            .Include(u => u.User)
            .Include(u => u.StatusProduct)
            .Include(u => u.Ypk)
            .OrderBy(x => x.Id)
            .Where(x => x.StatusProduct.StatusName.Contains(nameof(StatusProductEnum.Editing)))
            .ProjectTo<ProductLookupDto>(mapper.ConfigurationProvider)
            .AsQueryable();

        return await query.ToPagedListAsync(request.Page, request.PageSize, cancellationToken);
    }
}
