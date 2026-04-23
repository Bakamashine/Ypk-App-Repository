using Application.Dtos.Ypks;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Ypks.GetYpkList;

public class GetAllYpkQueryHandler : IRequestHandler<GetAllYpkQuery, YpkListVm>
{
    private readonly IProductsDbContext context;
    private readonly IMapper mapper;

    public GetAllYpkQueryHandler(IProductsDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<YpkListVm> Handle(GetAllYpkQuery request, CancellationToken cancellationToken)
    {
        var ypkQuery = await context.Ypks
            .Where(x => x.IsActive == true)
            .ProjectTo<YpkLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new YpkListVm { Ypks = ypkQuery };
    }
}