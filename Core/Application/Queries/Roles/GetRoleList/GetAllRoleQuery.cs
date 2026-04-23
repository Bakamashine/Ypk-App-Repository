using Application.Dtos.Roles;
using MediatR;

namespace Application.Common.Queries.Roles.GetRoleList;

public class GetAllRoleQuery : IRequest<RoleListVm>
{
}