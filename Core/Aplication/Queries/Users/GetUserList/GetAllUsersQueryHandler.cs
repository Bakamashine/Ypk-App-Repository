using Aplication.Dtos.Users;
using Aplication.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Users.GetUserList
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUserQuery, UserListVm>
    {
        private readonly IMapper mapper;
        private readonly IProductsDbContext context;

        public GetAllUsersQueryHandler(IMapper mapper, IProductsDbContext context)
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
}
