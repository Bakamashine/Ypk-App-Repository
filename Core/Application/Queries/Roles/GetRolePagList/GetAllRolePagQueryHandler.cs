using Application.Dtos.Roles;
using Application.Extensions;
using Application.Interfaces;
using Application.Queries.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Common.Queries.Roles.GetRolePagList;

public class GetAllRolePagQueryHandler : IRequestHandler<GetAllRolePagQuery, PagedList<RoleLookupDto>>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAllRolePagQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<PagedList<RoleLookupDto>> Handle(GetAllRolePagQuery request, CancellationToken cancellationToken)
    {
        var query = context.Roles
            .ProjectTo<RoleLookupDto>(mapper.ConfigurationProvider)
            .AsQueryable();

        return await query.ToPagedListAsync(request.Page, request.PageSize, cancellationToken);
    }
}
