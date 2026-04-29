using Application.Dtos.Roles;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Queries.Roles.GetRoleList;

public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, RoleListVm>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAllRoleQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<RoleListVm> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
    {
        var rolesQuery = await context.Roles
            .ProjectTo<RoleLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new RoleListVm { Roles = rolesQuery };
    }
}