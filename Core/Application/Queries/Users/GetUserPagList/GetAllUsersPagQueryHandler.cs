using Application.Dtos.Users;
using Application.Extensions;
using Application.Interfaces;
using Application.Queries.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Users.GetUserPagList;

public class GetAllUsersPagQueryHandler : IRequestHandler<GetAllUserPagQuery, PagedList<UserLookupDto>>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAllUsersPagQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<PagedList<UserLookupDto>> Handle(GetAllUserPagQuery request, CancellationToken cancellationToken)
    {
        var query = context.Users
            .Include(u => u.Role)
            .Where(x => x.IsActive == true)
            .OrderBy(x => x.Id)
            .ProjectTo<UserLookupDto>(mapper.ConfigurationProvider)
            .AsQueryable();

        return await query.ToPagedListAsync(request.Page, request.PageSize, cancellationToken);
    }
}
