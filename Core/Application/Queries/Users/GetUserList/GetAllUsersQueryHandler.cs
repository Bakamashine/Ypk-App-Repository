using Application.Dtos.Users;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Users.GetUserList;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUserQuery, UserListVm>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAllUsersQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<UserListVm> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var users = await context.Users
            .Include(u => u.Role)
            .Where(x => x.IsActive == true)
            .OrderBy(x => x.Id)
            .ProjectTo<UserLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new UserListVm { Users = users };
    }
}