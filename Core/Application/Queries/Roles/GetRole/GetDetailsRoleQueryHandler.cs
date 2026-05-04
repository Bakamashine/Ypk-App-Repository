using Application.Common.Exceptions;
using Application.Dtos.Roles;
using Application.Interfaces;
using AutoMapper;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Queries.Roles.GetRole;

public class GetDetailsRoleQueryHandler : IRequestHandler<GetDetailsRoleQuery, RoleLookupDto>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetDetailsRoleQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<RoleLookupDto> Handle(GetDetailsRoleQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await context.Users.FindAsync(new object[] { request.CurrentUserId }, cancellationToken)
                          ?? throw new NotFoundException(nameof(User), request.CurrentUserId);
        var roleUser = await context.Roles.FirstOrDefaultAsync(r => r.Id == currentUser.RoleId, cancellationToken)
                       ?? throw new NotFoundException(nameof(Role), currentUser.RoleId);

        if (roleUser.RoleName == "Admin")
        {
            var entity = await context.Roles
                             .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                         ?? throw new NotFoundException(nameof(Role), request.Id);
            return mapper.Map<RoleLookupDto>(entity);
        }

        throw new AccessException();
    }
}