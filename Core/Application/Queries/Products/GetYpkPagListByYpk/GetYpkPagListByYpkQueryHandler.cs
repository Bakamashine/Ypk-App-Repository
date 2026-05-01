using Application.Dtos.Products;
using Application.Extensions;
using Application.Interfaces;
using Application.Queries.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Products.GetYpkPagListByYpk;

public class GetYpkPagListByYpkQueryHandler : IRequestHandler<GetYpkPagListByYpkQuery, PagedList<ProductLookupDto>>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetYpkPagListByYpkQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<PagedList<ProductLookupDto>> Handle(GetYpkPagListByYpkQuery request, CancellationToken cancellationToken)
    {
        var query = context.Products
            .Include(u => u.User)
            .Include(u => u.StatusProduct)
            .Include(u => u.Ypk)
            .Where(x => x.StatusProduct.StatusName.Contains(nameof(StatusProductEnum.Publish)) &&
                        request.YpkId == x.YpkId)
            .OrderBy(x => x.Id)
            .ProjectTo<ProductLookupDto>(mapper.ConfigurationProvider)
            .AsQueryable();

        return await query.ToPagedListAsync(request.Page, request.PageSize, cancellationToken);
    }
}

public class GetYpkPagListByYpkQueryValidator : AbstractValidator<GetYpkPagListByYpkQuery>
{
    public GetYpkPagListByYpkQueryValidator()
    {
        RuleFor(x => x.YpkId).NotEmpty().NotNull();
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}
