using Aplication.Dtos.Users;
using Aplication.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Users.GetUser
{
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
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return mapper.Map<UserLookupDto>(entity);
        }
    }
}
