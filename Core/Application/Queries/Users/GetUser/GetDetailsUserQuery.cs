using Application.Dtos.Users;
using MediatR;

namespace Application.Queries.Users.GetUser;

public class GetDetailsUserQuery : IRequest<UserLookupDto>
{
    public Guid Id { get; set; }
}