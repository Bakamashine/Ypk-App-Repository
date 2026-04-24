using Application.Dtos.Users;
using MediatR;

namespace Application.Queries.Users.GetUserList;

public class GetAllUserQuery : IRequest<UserListVm>
{
}