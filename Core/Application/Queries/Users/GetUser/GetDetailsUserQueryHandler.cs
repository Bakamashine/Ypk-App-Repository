using Application.Common.Exceptions;
using Application.Dtos.Users;
using Application.Interfaces;
using AutoMapper;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Users.GetUser;

public class GetDetailsUserQueryHandler : IRequestHandler<GetDetailsUserQuery, UserLookupDto>
{
    private readonly IProductsDbContext context;
    private readonly IMapper mapper;

    public GetDetailsUserQueryHandler(IProductsDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<UserLookupDto> Handle(GetDetailsUserQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Users
                         .Include(u => u.Role)
                         .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ??
                     throw new NotFoundException(nameof(User), request.Id);

        return mapper.Map<UserLookupDto>(entity);
    }
}