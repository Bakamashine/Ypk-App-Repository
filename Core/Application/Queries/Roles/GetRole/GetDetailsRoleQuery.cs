using Application.Dtos.Roles;
using MediatR;

namespace Application.Common.Queries.Roles.GetRole;

public class GetDetailsRoleQuery : IRequest<RoleLookupDto>
{
    public Guid Id { get; set; }
    public Guid CurrentUserId { get; set; }
}