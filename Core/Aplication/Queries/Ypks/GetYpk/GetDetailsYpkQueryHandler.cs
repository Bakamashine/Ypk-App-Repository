using Aplication.Dtos.Roles;
using Aplication.Dtos.Ypks;
using Aplication.Interfaces;
using Application.Common.Exceptions;
using Application.Common.Queries.Roles.GetRole;
using AutoMapper;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Ypks.GetYpk
{
    public class GetDetailsYpkQueryHandler : IRequestHandler<GetDetailsYpkQuery, YpkLookupDto>
    {
        private readonly IProductsDbContext context;
        private readonly IMapper mapper;

        public GetDetailsYpkQueryHandler(IProductsDbContext context, IMapper mapper)
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
}
