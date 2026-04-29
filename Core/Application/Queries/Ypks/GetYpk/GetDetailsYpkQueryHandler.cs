using Application.Common.Exceptions;
using Application.Dtos.Ypks;
using Application.Interfaces;
using AutoMapper;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Ypks.GetYpk;

public class GetDetailsYpkQueryHandler : IRequestHandler<GetDetailsYpkQuery, YpkLookupDto>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetDetailsYpkQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<YpkLookupDto> Handle(GetDetailsYpkQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Ypks
                         .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                     ?? throw new NotFoundException(nameof(Role), request.Id);
        return mapper.Map<YpkLookupDto>(entity);
    }
}