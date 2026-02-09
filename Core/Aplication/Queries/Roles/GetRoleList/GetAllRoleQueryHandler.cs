using Aplication.Dtos.Roles;
using Aplication.Interfaces;
using Application.Common.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Queries.Roles.GetRoleList
{
    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, RoleListVm>
    {
        private readonly IProductsDbContext context;
        private readonly IMapper mapper;

        public GetAllRoleQueryHandler(IProductsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<RoleListVm> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
                var rolesQuery = await context.Roles
                .ProjectTo<RoleLookupDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

                return new RoleListVm { Roles = rolesQuery };

        }
    }
}
